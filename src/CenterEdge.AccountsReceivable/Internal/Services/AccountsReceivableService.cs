using CenterEdge.AccountsReceivable.Internal.Helpers;
using CenterEdge.AccountsReceivable.Models;
using CenterEdge.AccountsReceivable.Ports;
using CenterEdge.Common.Results;
using CenterEdge.Common.Results.Codes;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CenterEdge.AccountsReceivable.Internal.Services
{
    // TODO:
    internal class AccountsReceivableService //: IAccountsReceivableService
    {
        #region fields
        private readonly IResultFactory _resultFactory;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        #endregion

        #region ctor
        public AccountsReceivableService(IResultFactory resultFactory, ILoggerFactory loggerFactory)
        {
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = _loggerFactory.CreateLogger<AccountsReceivableService>();
        }
        #endregion

        #region public methods
        /// <summary>
        /// Adds a purchase to an account.
        /// </summary>
        /// <param name="accountHolder"></param>
        /// <param name="purchase"></param>
        /// <returns></returns>
        //public async Task<Result> MakePurchase(AccountHolder accountHolder, Purchase purchase)
        //{
        //    _logger.LogDebug("Beginning A/R Purchase");
        //    // TODO: Save the changes
        //    try
        //    {
        //        Transaction transaction = TransactionFactory.GetPurchaseTransaction(
        //            accountHolder.PurchaseAccount.AccountId, purchase.Amount, purchase.RefId);

        //        if (!transaction.IsValid())
        //            return _resultFactory.Create(new ResultCode(), new Error("Transaction does not balance"));

        //        accountHolder.PurchaseAccount.Purchases.Add(purchase);
        //        await accountHolder.ApplyTransaction(transaction);
        //        return _resultFactory.Success();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return _resultFactory.Exception(ApplicationSegment.None, 0, ex);
        //    }
        //}

        /// <summary>
        /// Applies a payment to an invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="amount"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        //public async Task<Result> MakeDeposit(AccountHolder accountHolder, Decimal amount, Guid? refId)
        //{
        //    _logger.LogDebug("Beginning A/R Deposit");
        //    // TODO: Save the changes
        //    try
        //    {
        //        Transaction transaction = TransactionFactory.GetDepositTransaction(
        //            accountHolder.CreditAccount.AccountId, amount, refId);

        //        if (!transaction.IsValid())
        //            return _resultFactory.Create(new ResultCode(), new Error("Transaction does not balance"));

        //        await accountHolder.ApplyTransaction(transaction);
        //        return _resultFactory.Success();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return _resultFactory.Exception(ApplicationSegment.None, 0, ex);
        //    }
        //}

        /// <summary>
        /// Applies a payment to an invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="amount"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        //public async Task<Result> MakePayment(AccountHolder accountHolder, IInvoice invoice, Decimal amount, Guid? refId)
        //{
        //    _logger.LogDebug("Beginning A/R Payment");
        //    // TODO: Save the changes
        //    try
        //    {
        //        var transaction = TransactionFactory.GetPaymentTransaction(invoice.AccountId, amount, null);

        //        if (!transaction.IsValid())
        //            return _resultFactory.Create(new ResultCode(), new Error("Transaction does not balance"));

        //        await accountHolder.ApplyTransaction(transaction);
        //        return _resultFactory.Success();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return _resultFactory.Exception(ApplicationSegment.None, 0, ex);
        //    }
        //}

        /// <summary>
        /// Writes off all the value on an account
        /// </summary>
        /// <param name="accountHolder"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        //public async Task<Result> WriteOffAccount(AccountHolder accountHolder, Guid accountId)
        //{
        //    _logger.LogDebug("Beginning A/R Write-Off");
        //    // TODO: Save the changes
        //    var transaction = TransactionFactory.GetWriteOffTransaction(Common.WriteOffAccountId, accountId, 0, null);

        //    if (!transaction.IsValid())
        //        return _resultFactory.Create(new ResultCode(), new Error("Transaction does not balance"));

        //    await accountHolder.ApplyTransaction(transaction);
        //    return _resultFactory.Success();
        //}

        /// <summary>
        /// Creates a new invoice account for all uninvoiced purchases.
        /// </summary>
        /// <param name="accountHolder">The AccountHolder to process</param>
        /// <returns></returns>
        //public async Task<Result> InvoiceOutstandingPurchases(AccountHolder accountHolder)
        //{
        //    _logger.LogDebug("Beginning A/R Invoice Purchases");
        //    // TODO: Save the changes
        //    try
        //    {
        //        return await Task.Run(() =>
        //        {
        //            if (accountHolder.PurchaseAccount.Purchases.Count > 0)
        //                accountHolder.InvoiceAccounts.Add(new InvoiceAccount(accountHolder.PurchaseAccount, DateTime.Now));

        //            accountHolder.PurchaseAccount = new UninvoicedPurchaseAccount();
        //            return _resultFactory.Success();
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return _resultFactory.Exception(ApplicationSegment.None, 0, ex);
        //    }
        //}

        /// <summary>
        /// Creates a payment schedule for the invoiced account
        /// </summary>
        /// <param name="accountHolder"></param>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public async Task<IResult> CreatePaymentSchedule(AccountHolder accountHolder, IInvoice invoice)
        {
            _logger.LogDebug("Beginning A/R Create Payment Schedule");
            // TODO: Save the changes
            try
            {
                return await Task.Run(() =>
                {
                    // TODO: Create the schedule
                    return _resultFactory.Success();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return _resultFactory.Exception(ApplicationSegment.None, 0, ex);
            }
        }
        #endregion
    }
}
