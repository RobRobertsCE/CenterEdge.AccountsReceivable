using CenterEdge.AccountsReceivable.Models;
using CenterEdge.AccountsReceivable.Ports;
using CenterEdge.Common.Results;
using CenterEdge.Common.Results.Codes;
using System;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Internal.Helpers
{
    internal abstract class InvoicePaymentStrategy
    {
        IResultFactory _resultFactory;

        public InvoicePaymentStrategy(IResultFactory resultFactory)
        {
            _resultFactory = resultFactory;
        }

        public abstract Task<Result> ApplyPaymentToInvoiceAsync(IInvoice invoice, decimal amount, Guid? refId);
        
        //protected async virtual Task<Result<Entry>> GetCreditAccountDepositEntry(Guid accountHolderId, decimal depositAmount)
        //{
        //    try
        //    {
        //        var accountHolder = await _accountHolderRepository.GetAccountHolderAsync(accountHolderId);

        //        var newEntry = new Entry()
        //        {
        //            AccountId = accountHolder.CreditAccount.AccountId,
        //            Amount = depositAmount
        //        };

        //        return _resultFactory.Success<Entry>(newEntry);
        //    }
        //    catch (Exception ex)
        //    {
        //        return _resultFactory.Exception<Entry>(ApplicationSegment.None, 0, ex);
        //    }
           
        //}
    }
}
