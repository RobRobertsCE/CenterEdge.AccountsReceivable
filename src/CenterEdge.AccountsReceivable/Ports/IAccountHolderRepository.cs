using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Ports
{
    public interface IAccountHolderRepository
    {
        Task<IResult<IEnumerable<AccountHolder>>> GetAccountHoldersAsync();
        Task<IResult<AccountHolder>> GetAccountHolderAsync(Guid id);
        Task<IResult<AccountHolder>> SaveAccountHolderAsync(AccountHolder accountHolder);
    }
}
