using Microsoft.Extensions.DependencyInjection;
using System;
using CenterEdge.AccountsReceivable;

namespace TestApp2
{
    public static class ApplicationServices
    {
        public static IServiceProvider Container { get; private set; }

        public static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddAccountsReceivableServices();

            Container = services.BuildServiceProvider();
        }
    }
}
