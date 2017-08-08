using CenterEdge.Common.Results;
using CenterEdge.Common.Results.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Models
{
    public abstract class Account
    {
        private readonly IResultFactory _resultFactory;

        public Guid AccountId { get; set; }
        public Guid AccountHolderId { get; set; }
        public AccountType AccountType { get; set; }
        public string Name { get; set; }
        public AccountStatus Status { get; set; }
        public IList<Entry> Entries { get; set; }

        public Account(IResultFactory resultFactory)
        {
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
            Entries = new List<Entry>();
        }

        public abstract Decimal GetBalance();

        //public async Task<Result<Transaction>> ApplyTransaction(Transaction transaction)
        //{
        //    try
        //    {
        //        if (!transaction.IsValid())
        //            throw new Exception("Transaction does not balance");

        //        await Task.Run(() =>
        //        {
        //            ApplyEntries(transaction.Entries.Where(e => e.AccountId == AccountId));
        //        });

        //        return _resultFactory.Success<Transaction>(transaction);
        //    }
        //    catch (Exception ex)
        //    {
        //        return _resultFactory.Exception<Transaction>(ApplicationSegment.None, 0, ex);
        //    }
        //}

        internal  void ApplyEntries(IEnumerable<Entry> entriesToApply)
        {
            foreach (var unappliedEntry in entriesToApply)
            {
                ApplyEntry(unappliedEntry);
            }
        }

        internal void ApplyEntry(Entry entryToApply)
        {
            Entries.Add(entryToApply);
        }
    }
}
