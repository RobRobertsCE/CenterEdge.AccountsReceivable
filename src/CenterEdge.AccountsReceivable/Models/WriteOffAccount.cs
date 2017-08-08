using CenterEdge.Common.Results;
using System;

namespace CenterEdge.AccountsReceivable.Models
{
    public class WriteOffAccount : Account
    {
        public WriteOffAccount(IResultFactory resultFactory)
            : base(resultFactory)
        {

        }

        public override decimal GetBalance()
        {
            throw new InvalidOperationException("Account balance does not apply to global Write-Off account.");
        }
    }
}
