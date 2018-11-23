using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Cash_Register_Divyansh.BusinessLogic;
using Cash_Register_Divyansh.Contracts.Business;
using Cash_Register_Divyansh.Contracts.Data;
using Cash_Register_Divyansh.Repositories;

namespace Cash_Register_Divyansh.Utility
{
    public class AppStartUtility
    {
        public static IConfigurationRoot Configuration;
        public static ServiceProvider LoadAndRegisterDependencies()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }


        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Build configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection.AddSingleton(Configuration);

            // Add services
            serviceCollection.AddTransient<ICashRegisterManager, CashRegisterManager>();
            serviceCollection.AddTransient<ICashRegisterRepository, CashRegisterRepository>();
            serviceCollection.AddTransient<IItemScannerManager, ItemScannerManager>();
            serviceCollection.AddTransient<ISummaryDisplayManager, SummaryDisplayManager>();

        }
    }
}
