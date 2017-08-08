using NodaTime;
using System;

namespace CenterEdge.AccountsReceivable.Models
{
    public class Purchase
    {
        public Guid PurchaseId { get; set; }
        public PurchaseType PurchaseType { get; set; }
        public Guid AccountId { get; set; }
        public Decimal Amount { get; set; }
        public Instant Created { get; set; }
        public Guid? RefId { get; set; }
    }
}
