using CenterEdge.AccountsReceivable.Ports;
using System;
using System.Collections.Generic;
using CenterEdge.AccountsReceivable.Models;
using System.Threading.Tasks;
using CenterEdge.Common.Results;

namespace CenterEdge.AccountsReceivable.Internal.Services
{
    public class TransactionService : ITransactionService
    {
        private ITransactionValidationService _transactionValidationService;
        private IAccountRepository _accountRepository;

        public TransactionService(ITransactionValidationService validationService, IAccountRepository accountRepository)
        {
            _transactionValidationService = validationService;
            _accountRepository = accountRepository;
        }
        
        public async Task<Result> ApplyTransaction(Transaction transaction) // AccountHolder accountHolder
        {
            try
            {
                var accounts = new Dictionary<Guid, Account>();
                
                var validationResult = _transactionValidationService.ValidateTransaction(transaction);

                if (validationResult.IsSuccessful) // TODO: Convert to Result<T>
                {
                    foreach (var entry in transaction.Entries)
                    {
                        Account account;
                        if (!accounts.TryGetValue(entry.AccountId, out account))
                        {
                            account = await _accountRepository.GetAccountAsync(entry.AccountId);
                        }

                        if (null == account)
                        {
                            throw new InvalidOperationException("Invalid AccountId");
                        }
                        accounts.Add(account.AccountId, account);
                        account.Entries.Add(entry);
                    }       
                }

                return _resultFactory.Success<Entry>(newEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
