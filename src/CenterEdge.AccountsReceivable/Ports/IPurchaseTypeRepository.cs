using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Ports
{
    public interface IPurchaseTypeRepository
    {
        Task<IResult<PurchaseType>> GetPurchaseTypeAsync(Guid id);
        Task<IResult<IEnumerable<PurchaseType>>> GetPurchaseTypesAsync();
    }
}
