using CenterEdge.Common.Results;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CenterEdge.AccountsReceivable.Models
{
    public class InvoiceAccount : Account, IInvoice
    {
        public String InvoiceNumber { get; set; }
        public Instant Created { get; set; }
        public bool IsAutoInvoice { get; set; }
        public IList<Purchase> Purchases { get; set; }
        public IList<ScheduledPayment> ScheduledPayments { get; set; }

        public InvoiceAccount(IResultFactory resultFactory)
            : base(resultFactory)
        {
            ScheduledPayments = new List<ScheduledPayment>();
        }

        public override decimal GetBalance()
        {
            var purchases = Purchases.Sum(p => p.Amount);
            var payments = Entries.Sum(e => e.Amount);
            return purchases - payments;
        }
    }
}
