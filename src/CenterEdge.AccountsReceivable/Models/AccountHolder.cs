using CenterEdge.Common.Results;
using CenterEdge.Common.Results.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Models
{
    public class AccountHolder
    {
        private readonly IResultFactory _resultFactory;

        public Guid AccountHolderId { get; set; }
        public AccountHolderType AccountHolderType { get; set; }
        public AccountHolderStatus Status { get; set; }
        public string Description { get; set; }
        public CreditAccount CreditAccount { get; set; }
        public IList<InvoiceAccount> InvoiceAccounts { get; set; }

        public AccountHolder(IResultFactory resultFactory)
        {
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
            InvoiceAccounts = new List<InvoiceAccount>();
        }

        public async Task<IResult<Transaction>> ApplyTransaction(Transaction transaction)
        {
            // TODO: Validation? Check accountholder status? account status? All entries applied?
            try
            {
                await Task.Run(() =>
                {
                    if (!transaction.IsValid())
                        throw new Exception("Transaction does not balance");

                    // Credit Account
                    if (transaction.TransactionType == TransactionType.Deposit ||
                         transaction.TransactionType == TransactionType.Transfer)
                    {
                        CreditAccount.ApplyEntries(transaction.Entries.Where(e => e.AccountId == CreditAccount.AccountId));
                    }

                    // Invoice Accounts
                    if (transaction.TransactionType == TransactionType.Transfer ||
                        transaction.TransactionType == TransactionType.Payment)
                    {
                        foreach (var invoiceAccount in InvoiceAccounts)
                        {
                            invoiceAccount.ApplyEntries(transaction.Entries.Where(e => e.AccountId == invoiceAccount.AccountId));
                        }
                    }

                    // global accts
                    if (transaction.TransactionType == TransactionType.WriteOff)
                    {
                        // TODO: WriteOffAccount? Or Apply externally?

                        //foreach (var unappliedEntry in transaction.Entries.Where(e => e.IsApplied == false))
                        //{
                        //    var account = InvoiceAccounts.FirstOrDefault(a => a.AccountId == unappliedEntry.AccountId);
                        //    if (null == account)
                        //        throw new InvalidOperationException("Unrecognized AccountId in transaction");
                        //    ((Account)account).ApplyEntry(unappliedEntry);
                        //}
                    }
                });

                return _resultFactory.Success<Transaction>(transaction);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<Transaction>(ApplicationSegment.None, 0, ex);
            }
        }
    }
}
