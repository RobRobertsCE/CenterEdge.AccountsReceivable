using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Ports
{
    public interface ITransactionValidationService
    {
        Result ValidateTransaction(Transaction transaction);
    }
}
