using CenterEdge.AccountsReceivable.Models;
using NodaTime;
using System;

namespace CenterEdge.AccountsReceivable.Internal.Helpers
{
    internal static class TransactionFactory
    {
        #region public methods
        /// <summary>
        /// Creates a new transaction for a purchase charged to an A/R account
        /// </summary>
        /// <param name="purchaseAccountId">Id of the Purchase Account to charge the purchase to</param>
        /// <param name="amount">The amount of the purchase</param>
        /// <param name="refId">Optional reference id, varies by transaction source</param>
        /// <returns>Instance of Transaction, containing entries.</returns>
        public static Transaction GetPurchaseTransaction(Guid purchaseAccountId, Decimal amount, Guid? refId)
        {
            var transaction = GetTransaction(TransactionType.Purchase, amount, refId);
            transaction.Entries.Add(GetEntry(purchaseAccountId, -amount));

            return transaction;
        }

        /// <summary>
        /// Creates a new transaction to make a payment on an A/R invoice
        /// </summary>
        /// <param name="invoiceAccountId">Id of the Invoice Account to apply the payment to</param>
        /// <param name="amount">The amount of payment to be applied</param>
        /// <param name="refId">Optional reference id, varies by transaction source</param>
        /// <returns>Instance of Transaction, containing entries.</returns>
        public static Transaction GetPaymentTransaction(Guid invoiceAccountId, Decimal amount, Guid? refId)
        {
            var transaction = GetTransaction(TransactionType.Payment, amount, refId);
            transaction.Entries.Add(GetEntry(invoiceAccountId, -amount));

            return transaction;
        }

        /// <summary>
        /// Creates a transaction which makes an invoice payment using credit account funds
        /// </summary>
        /// <param name="creditAccountId">Id of the Credit Account to take the payment from</param>
        /// <param name="invoiceAccountId">Id of the Invoice Accountccount to apply the payment to</param>
        /// <param name="amount">The amount of payment to be applied</param>
        /// <param name="refId">Optional reference id, varies by transaction source</param>
        /// <returns>Instance of Transaction, containing entries.</returns>
        public static Transaction GetPaymentFromCreditTransaction(Guid creditAccountId, Guid invoiceAccountId, Decimal amount, Guid? refId)
        {
            var transaction = GetTransaction(TransactionType.Transfer, 0, refId);
            transaction.Entries.Add(GetEntry(creditAccountId, amount));
            transaction.Entries.Add(GetEntry(invoiceAccountId, -amount));

            return transaction;
        }

        /// <summary>
        /// Creates a transaction to deposit funds into a credit account
        /// </summary>
        /// <param name="creditAccountId">Id of the CreditAccount to make the deposit in</param>
        /// <param name="amount">The amount to add to the CreditAccount</param>
        /// <param name="refId">Optional reference id, varies by transaction source</param>
        /// <returns>Instance of Transaction, containing entries.</returns>
        public static Transaction GetDepositTransaction(Guid creditAccountId, Decimal amount, Guid? refId)
        {
            var transaction = GetTransaction(TransactionType.Deposit, -amount, refId);
            transaction.Entries.Add(GetEntry(creditAccountId, -amount));

            return transaction;
        }

        /// <summary>
        /// Creates a transaction where an account's balance is written off
        /// </summary>
        /// <param name="globalWriteOffAccountId">Id of the global Write-Off account</param>
        /// <param name="accountId">Id of the account to write off</param>
        /// <param name="amount">The amount to write off</param>
        /// <param name="refId">Optional reference id, varies by transaction source</param>
        /// <returns>Instance of Transaction, containing entries.</returns>
        public static Transaction GetWriteOffTransaction(Guid globalWriteOffAccountId, Guid accountId, Decimal amount, Guid? refId)
        {
            var transaction = GetTransaction(TransactionType.WriteOff, amount, refId);
            transaction.Entries.Add(GetEntry(globalWriteOffAccountId, amount));
            transaction.Entries.Add(GetEntry(accountId, -amount));

            return transaction;
        }
        #endregion

        #region private methods
        private static Entry GetEntry(Guid accountId, Decimal amount)
        {
            return new Entry()
            {
                AccountId = accountId,
                Amount = amount
            };
        }

        private static Transaction GetTransaction(TransactionType transactionType, Decimal netDifference, Guid? refId)
        {
            return GetTransaction(Guid.NewGuid(), Instant.FromDateTimeUtc(DateTime.Now), transactionType, netDifference, refId);
        }

        private static Transaction GetTransaction(Guid transactionId, Instant created, TransactionType transactionType, Decimal netDifference, Guid? refId)
        {
            return new Transaction()
            {
                TransactionId = transactionId,
                Created = created,
                TransactionType = transactionType,
                NetDifference = netDifference,
                RefId = refId
            };
        }
        #endregion
    }
}
