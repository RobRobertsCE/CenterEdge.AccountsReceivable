using System;
using System.Threading.Tasks;
using CenterEdge.AccountsReceivable.Models;
using System.Linq;
using CenterEdge.Common.Results;

namespace CenterEdge.AccountsReceivable.Internal.Helpers
{
    internal class DistributedPaymentStrategy : InvoicePaymentStrategy
    {
        public DistributedPaymentStrategy(IResultFactory resultFactory)
            : base(resultFactory)
        {
        }

        public async override Task<Result> ApplyPaymentToInvoiceAsync(IInvoice invoice, decimal amount, Guid? refId)
        {
            Transaction transaction = TransactionFactory.GetNewInvoicePaymentTransaction(amount, refId);

            var outstandingPurchases = invoice.Purchases.Where(p => p.Balance > 0);
            var remainingPaymentBalance = amount;
            var paymentAmount = amount / outstandingPurchases.Count();
            // TODO: Rounding?
            foreach (var purchase in outstandingPurchases) // TODO: Check status?
            {
                if (remainingPaymentBalance < paymentAmount)
                    paymentAmount = remainingPaymentBalance;

                transaction.Entries.Add(new Entry()
                {
                    AccountId = invoice.AccountId,
                    Amount = paymentAmount
                });

                remainingPaymentBalance -= remainingPaymentBalance;

                if (remainingPaymentBalance <= 0)
                    break;
            }

            if (remainingPaymentBalance > 0)
            {
                var creditAccountDepositEntry = await GetCreditAccountDepositEntry(invoice.AccountHolderId, remainingPaymentBalance);
                transaction.Entries.Add(creditAccountDepositEntry);
            }

            var validationResult = IsTransactionValid(transaction).Result;

            if (true == validationResult)
            {
                invoice.ApplyTransaction(transaction);
            }
        }
    }
}
