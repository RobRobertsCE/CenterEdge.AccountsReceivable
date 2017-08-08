using CenterEdge.Common.Results;
using System.Linq;

namespace CenterEdge.AccountsReceivable.Models
{
    public class CreditAccount : Account
    {
        public CreditAccount(IResultFactory resultFactory)
            : base(resultFactory)
        {

        }

        public override decimal GetBalance()
        {
            return base.Entries.Sum(e => e.Amount);
        }
    }
}
