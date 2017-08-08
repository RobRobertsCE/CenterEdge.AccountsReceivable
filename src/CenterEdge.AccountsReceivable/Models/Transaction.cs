using CenterEdge.Common.Results;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Instant Created { get; set; }    // TODO: Add to Model
        public TransactionType TransactionType { get; set; } // TODO: Add to Model. Tells what the RefId links to
        public Decimal NetDifference { get; set; }
        public Guid? RefId { get; set; }
        public IList<Entry> Entries { get; set; }

        public Transaction()
        {
            Entries = new List<Entry>();
        }

        public bool IsValid()
        {
            return (Entries.Sum(e => e.Amount) == NetDifference);
        }
    }
}
