using Cash_Register_Divyansh.Contracts.Business;
using Cash_Register_Divyansh.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cash_Register_Divyansh
{
    /// <summary>
    /// Entry point of the application
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //load Dependencies
            var serviceProvider = AppStartUtility.LoadAndRegisterDependencies();
            Console.WriteLine("Welcome to Custom Cash Register. Press enter to start scanning or type Exit while entering item name to check out or exit the application");
            var keepScanning = CommonUtility.ContinueToScan(Console.ReadLine());
            if (keepScanning)
            {
                var cashRegManager = serviceProvider.GetService<ICashRegisterManager>();
                cashRegManager.StartProcess();
            }
            else
            {
                //code will exit
                Console.WriteLine("Thank you!");
                Console.Read();
            }
        }
    }
}
