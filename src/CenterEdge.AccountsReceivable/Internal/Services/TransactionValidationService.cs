using CenterEdge.AccountsReceivable.Ports;
using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System.Linq;
using CenterEdge.Common.Results.Codes;

namespace CenterEdge.AccountsReceivable.Internal.Services
{
    internal class TransactionValidationService : ITransactionValidationService
    {
        IResultFactory _resultFactory;

        public TransactionValidationService(IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
        }

        public Result ValidateTransaction(Transaction transaction)
        {
            if (transaction.Entries.Sum(e => e.Amount) == transaction.NetDifference)
                return _resultFactory.Success();
            else
                return _resultFactory.Create(new ResultCode(), new Error("Transaction does not balance"));
        }
    }
}
