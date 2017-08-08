using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Ports
{
    public interface IAccountHolderTypeRepository
    {
        Task<IResult<AccountHolderType>> GetAccountHolderTypeAsync(Guid id);
        Task<IResult<IEnumerable<AccountHolderType>>> GetAccountHolderTypesAsync();
    }
}
