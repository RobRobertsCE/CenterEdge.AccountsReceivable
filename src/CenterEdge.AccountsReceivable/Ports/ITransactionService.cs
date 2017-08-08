using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Ports
{
    public interface ITransactionService
    {
        Task<Result> ApplyTransaction(Transaction transaction);        
    }
}
