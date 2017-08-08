using CenterEdge.AccountsReceivable.Ports;
using System;
using System.Collections.Generic;
using CenterEdge.AccountsReceivable.Models;
using System.Threading.Tasks;
using CenterEdge.Common.Results;
using CenterEdge.Common.Results.Codes;
using System.Linq;

namespace CenterEdge.AccountsReceivable.Adapters
{
    /// <summary>
    /// Default repository for PurchaseType
    /// </summary>
    internal class PurchaseTypeRepository : IPurchaseTypeRepository
    {
        #region fields
        private IList<PurchaseType> _purchaseTypes;
        private readonly IResultFactory<PurchaseType> _resultFactory;
        #endregion

        #region ctor
        public PurchaseTypeRepository(IResultFactory<PurchaseType> resultFactory)
        {
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }
        #endregion

        #region public methods
        public async Task<IResult<PurchaseType>> GetPurchaseTypeAsync(Guid id)
        {
            return await Task.Run(() => GetPurchaseType(id));
        }

        public async Task<IResult<IEnumerable<PurchaseType>>> GetPurchaseTypesAsync()
        {
            return await Task.Run(() => GetPurchaseTypes());
        }
        #endregion

        #region protected methods
        protected virtual IResult<PurchaseType> GetPurchaseType(Guid id)
        {
            try
            {
                var internalList = GetInternalList();
                var internalItem = internalList.FirstOrDefault(i => i.Id == id);

                if (null == internalItem)
                    return _resultFactory.Create<PurchaseType>(
                        ResultCode.NoContent,
                        new List<Error>() { new Error(String.Format("No PurchaseType found with Id {0}", id)) }, null);
                else
                    return _resultFactory.Success(internalItem);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<PurchaseType>(ApplicationSegment.Database, 0, ex);
            }
        }

        protected virtual IResult<IEnumerable<PurchaseType>> GetPurchaseTypes()
        {
            try
            {
                var internalList = GetInternalList();
                return _resultFactory.Success<IEnumerable<PurchaseType>>(internalList);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<IEnumerable<PurchaseType>>(ApplicationSegment.Database, 1, ex);
            }
        }

        protected virtual IList<PurchaseType> GetInternalList()
        {
            if (null == _purchaseTypes)
            {
                _purchaseTypes = BuildInternalList();
            }

            return _purchaseTypes;
        }

        protected virtual IList<PurchaseType> BuildInternalList()
        {
            return new List<PurchaseType>()
            {
                new PurchaseType() { Id = Guid.NewGuid(), Name = "A" },
                new PurchaseType() { Id = Guid.NewGuid(), Name = "B" },
                new PurchaseType() { Id = Guid.NewGuid(), Name = "C" }
            };
        }
        #endregion
    }
}
