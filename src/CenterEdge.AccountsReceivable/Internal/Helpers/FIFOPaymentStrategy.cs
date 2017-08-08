using System;
using System.Threading.Tasks;
using CenterEdge.AccountsReceivable.Models;
using System.Linq;
using CenterEdge.Common.Results;

namespace CenterEdge.AccountsReceivable.Internal.Helpers
{
    internal class FIFOPaymentStrategy : InvoicePaymentStrategy
    {
        public FIFOPaymentStrategy(IResultFactory resultFactory) 
            : base(resultFactory)
        {
        }

        public override async Task<Result> ApplyPaymentToInvoiceAsync(IInvoice invoice, decimal amount, Guid? refId)
        {
            Transaction transaction = TransactionFactory.GetPaymentTransaction(invoice.AccountId, amount, refId);

            var outstandingPurchases = invoice.Purchases.Where(p => p.Balance > 0);// TODO: Check status?
            var remainingPaymentBalance = amount;
            decimal paymentAmount = 0;
            // TODO: Rounding?
            foreach (var purchase in outstandingPurchases.OrderByDescending(p => p.Created))
            {
                if (remainingPaymentBalance > purchase.Balance)
                    paymentAmount = purchase.Balance;
                else
                    paymentAmount = remainingPaymentBalance;

                remainingPaymentBalance -= paymentAmount;

                transaction.Entries.Add(new Entry()
                {
                    AccountId = invoice.AccountId,
                    Amount = paymentAmount
                });

                if (remainingPaymentBalance <= 0)
                    break;
            }

            if (remainingPaymentBalance > 0)
            {
                var creditAccountDepositEntry = await GetCreditAccountDepositEntry(invoice.AccountHolderId, remainingPaymentBalance);
                transaction.Entries.Add(creditAccountDepositEntry.Value);
            }

            var validationResult = IsTransactionValid(transaction).Result;

            if (validationResult.IsSuccessful)
            {
                invoice.ApplyTransaction(transaction);
            }
        }
    }
}
