using CenterEdge.AccountsReceivable.Ports;
using System;
using System.Collections.Generic;
using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System.Threading.Tasks;
using System.Linq;
using CenterEdge.Common.Results.Codes;

namespace CenterEdge.AccountsReceivable.Adapters
{
    /// <summary>
    /// Default repository for TransactionType
    /// </summary>
    internal class TransactionTypeRepository : ITransactionTypeRepository
    {
        #region fields
        private IList<TransactionType> _internalList;
        private readonly IResultFactory<TransactionType> _resultFactory;
        #endregion

        #region ctor
        public TransactionTypeRepository(IResultFactory<TransactionType> resultFactory)
        {
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }
        #endregion

        #region public methods
        public async Task<IResult<TransactionType>> GetTransactionTypeAsync(Guid id)
        {
            return await Task.Run(() => GetTransactionType(id));
        }

        public async Task<IResult<IEnumerable<TransactionType>>> GetTransactionTypesAsync()
        {
            return await Task.Run(() => GetTransactionTypes());
        }
        #endregion

        #region protected methods
        protected virtual IResult<TransactionType> GetTransactionType(Guid id)
        {
            try
            {
                var internalList = GetInternalList();
                var internalItem = internalList.FirstOrDefault(i => i.Id == id);

                if (null == internalItem)
                    return _resultFactory.Create<TransactionType>(
                        ResultCode.NoContent,
                        new List<Error>() { new Error(String.Format("No TransactionType found with Id {0}", id)) }, null);
                else
                    return _resultFactory.Success(internalItem);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<TransactionType>(ApplicationSegment.Database, 0, ex);
            }
        }

        protected virtual IResult<IEnumerable<TransactionType>> GetTransactionTypes()
        {
            try
            {
                var internalList = GetInternalList();
                return _resultFactory.Success<IEnumerable<TransactionType>>(internalList);
            }
            catch (Exception ex)
            {
                return _resultFactory.Exception<IEnumerable<TransactionType>>(ApplicationSegment.Database, 1, ex);
            }
        }

        protected virtual IList<TransactionType> GetInternalList()
        {
            if (null == _internalList)
            {
                _internalList = BuildInternalList();
            }

            return _internalList;
        }

        protected virtual IList<TransactionType> BuildInternalList()
        {
            return new List<TransactionType>()
            {
                new TransactionType() { Id = Guid.NewGuid(), Name = "Deposit" },
                new TransactionType() { Id = Guid.NewGuid(), Name = "Purchase" },
                new TransactionType() { Id = Guid.NewGuid(), Name = "Transfer" },
                new TransactionType() { Id = Guid.NewGuid(), Name = "Payment" },
                new TransactionType() { Id = Guid.NewGuid(), Name = "WriteOff" }
            };
        }
        #endregion
    }
}
