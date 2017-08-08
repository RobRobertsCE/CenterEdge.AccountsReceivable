using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Ports
{
    public interface IAccountRepository
    {
        Task<IResult<Account>> GetWriteOffAccountAsync();
    }
}
