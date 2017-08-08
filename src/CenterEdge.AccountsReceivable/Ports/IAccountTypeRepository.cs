using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Ports
{
    public interface IAccountTypeRepository
    {
        Task<IResult<AccountType>> GetAccountTypeAsync(Guid id);
        Task<IResult<IEnumerable<AccountType>>> GetAccountTypesAsync();
    }
}
