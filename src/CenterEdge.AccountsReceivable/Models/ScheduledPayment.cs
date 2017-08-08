using NodaTime;
using System;

namespace CenterEdge.AccountsReceivable.Models
{
    public class ScheduledPayment
    {
        public Guid ScheduledPaymentId { get; set; }
        public LocalDate DueDate { get; set; }
        public Decimal AmountDue { get; set; }
        public ScheduledPaymentStatus Status { get; set; }
        public Guid? RefId { get; set; }
    }
}
