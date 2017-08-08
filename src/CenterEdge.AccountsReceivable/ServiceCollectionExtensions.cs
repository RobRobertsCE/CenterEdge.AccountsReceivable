using CenterEdge.AccountsReceivable.Adapters;
using CenterEdge.AccountsReceivable.Internal.Services;
using CenterEdge.AccountsReceivable.Ports;
using CenterEdge.Common.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Abstractions;

namespace CenterEdge.AccountsReceivable
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountsReceivableServices(this IServiceCollection services)
        {
            // TODO: Add Logging?
            // services.TryAddTransient<IAccountsReceivableService, AccountsReceivableService>();

            services.AddResults();

            services.TryAddTransient<IAccountTypeRepository, AccountTypeRepository>();
            services.TryAddTransient<IPurchaseTypeRepository, PurchaseTypeRepository>();
            services.TryAddTransient<IAccountHolderTypeRepository, AccountHolderTypeRepository>();

            return services;
        }
    }
}
