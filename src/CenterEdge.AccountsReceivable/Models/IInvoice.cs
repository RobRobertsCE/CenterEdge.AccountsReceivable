using NodaTime;
using System;
using System.Collections.Generic;

namespace CenterEdge.AccountsReceivable.Models
{
    public interface IInvoice
    {
        Guid AccountId { get; set; }
        Guid AccountHolderId { get; set; }
        String Name { get; set; }
        Instant Created { get; set; }
        AccountStatus Status { get; set; }
        IList<Purchase> Purchases { get; set; }
        IList<ScheduledPayment> ScheduledPayments { get; set; }
        Decimal GetBalance();
    }
}
