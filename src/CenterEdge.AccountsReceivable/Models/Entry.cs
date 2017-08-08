using System;

namespace CenterEdge.AccountsReceivable.Models
{
    public class Entry
    {
        public Guid AccountId { get; set; }
        public Decimal Amount { get; set; }
    }
}
