using CenterEdge.AccountsReceivable.Models;
using CenterEdge.Common.Results;
using System;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Ports
{
    public interface IAccountsReceivableService
    {
        // TODO: Result objects?
        Task<IResult> CreatePaymentSchedule(AccountHolder accountHolder, IInvoice invoice);
        Task<IResult> InvoiceOutstandingPurchases(AccountHolder accountHolder);
        Task<IResult> MakeDeposit(AccountHolder accountHolder, decimal amount, Guid? refId);
        Task<IResult> MakePayment(AccountHolder accountHolder, IInvoice invoice, decimal amount, Guid? refId);
        Task<IResult> MakePurchase(AccountHolder accountHolder, Purchase purchase);
        Task<IResult> WriteOffAccount(AccountHolder accountHolder, Guid accountId);
    }
}
