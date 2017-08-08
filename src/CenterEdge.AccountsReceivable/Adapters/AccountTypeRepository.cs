using CenterEdge.AccountsReceivable.Ports;
using System;
using System.Collections.Generic;
using CenterEdge.AccountsReceivable.Models;
using System.Threading.Tasks;
using CenterEdge.Common.Results;
using System.Linq;
using CenterEdge.Common.Results.Codes;

namespace CenterEdge.AccountsReceivable.Adapters
{
    /// <summary>
    /// Default repository for AccountType
    /// </summary>
    internal class AccountTypeRepository : IAccountTypeRepository
    {
        #region fields
        private IList<AccountType> _internalList;
        private readonly IResultFactory<AccountType> _resultFactory;
        #endregion

        #region ctor
        public AccountTypeRepository(IResultFactory<AccountType> resultFactory)
        {
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }
        #endregion

        #region public methods
        public async Task<IResult<AccountType>> GetAccountTypeAsync(Guid id)
        {
            return await Task.Run(() => GetAccountType(id));
        }

        public async Task<IResult<IEnumerable<AccountType>>> GetAccountTypesAsync()
        {
            return await Task.Run(() => GetAccountTypes());
        }
        #endregion

        #region protected methods
        protected virtual IResult<AccountType> GetAccountType(Guid id)
        {
            try
            {
                var internalList = GetInternalList();
                var internalItem = internalList.FirstOrDefault(i => i.Id == id);

                if (null == internalItem)
                    return _resultFactory.Create<AccountType>(
                        ResultCode.NoContent,
                        new List<Error>() { new Error(String.Format("No AccountType found with Id {0}", id)) }, null);
                else
                    return _resultFactory.Success(internalItem);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<AccountType>(ApplicationSegment.Database, 0, ex);
            }
        }

        protected virtual IResult<IEnumerable<AccountType>> GetAccountTypes()
        {
            try
            {
                var internalList = GetInternalList();
                return _resultFactory.Success<IEnumerable<AccountType>>(internalList);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<IEnumerable<AccountType>>(ApplicationSegment.Database, 1, ex);
            }
        }

        protected virtual IList<AccountType> GetInternalList()
        {
            if (null == _internalList)
            {
                _internalList = BuildInternalList();
            }

            return _internalList;
        }

        protected virtual IList<AccountType> BuildInternalList()
        {
            return new List<AccountType>()
            {
                new AccountType() { Id = Guid.NewGuid(), Name = "A" },
                new AccountType() { Id = Guid.NewGuid(), Name = "B" },
                new AccountType() { Id = Guid.NewGuid(), Name = "C" }
            };
        }
        #endregion
    }
}
