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
    /// Default repository for AccountHolderType
    /// </summary>
    internal class AccountHolderTypeRepository : IAccountHolderTypeRepository
    {
        #region fields
        private IList<AccountHolderType> _internalList;
        private readonly IResultFactory<AccountHolderType> _resultFactory;
        #endregion

        #region ctor
        public AccountHolderTypeRepository(IResultFactory<AccountHolderType> resultFactory)
        {
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }
        #endregion

        #region public methods
        public async Task<IResult<AccountHolderType>> GetAccountHolderTypeAsync(Guid id)
        {
            return await Task.Run(() => GetAccountHolderType(id));
        }

        public async Task<IResult<IEnumerable<AccountHolderType>>> GetAccountHolderTypesAsync()
        {
            return await Task.Run(() => GetAccountHolderTypes());
        }
        #endregion

        #region protected methods
        protected virtual IResult<AccountHolderType> GetAccountHolderType(Guid id)
        {
            try
            {
                var internalList = GetInternalList();
                var internalItem = internalList.FirstOrDefault(i => i.Id == id);

                if (null == internalItem)
                    return _resultFactory.Create<AccountHolderType>(
                        ResultCode.NoContent,
                        new List<Error>() { new Error(String.Format("No AccountHolderType found with Id {0}", id)) }, null);
                else
                    return _resultFactory.Success(internalItem);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<AccountHolderType>(ApplicationSegment.Database, 0, ex);
            }
        }

        protected virtual IResult<IEnumerable<AccountHolderType>> GetAccountHolderTypes()
        {
            try
            {
                var internalList = GetInternalList();
                return _resultFactory.Success<IEnumerable<AccountHolderType>>(internalList);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<IEnumerable<AccountHolderType>>(ApplicationSegment.Database, 1, ex);
            }
        }

        protected virtual IList<AccountHolderType> GetInternalList()
        {
            if (null == _internalList)
            {
                _internalList = BuildInternalList();
            }

            return _internalList;
        }

        protected virtual IList<AccountHolderType> BuildInternalList()
        {
            return new List<AccountHolderType>()
            {
                new AccountHolderType() { Id = Guid.NewGuid(), Name = "Customer" },
                new AccountHolderType() { Id = Guid.NewGuid(), Name = "Chargeback" },
                new AccountHolderType() { Id = Guid.NewGuid(), Name = "Vendor" }
            };
        }
        #endregion
    }
}
