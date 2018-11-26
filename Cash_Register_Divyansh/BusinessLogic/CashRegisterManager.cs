using Cash_Register_Divyansh.ApplicationTypes;
using Cash_Register_Divyansh.Contracts.Business;
using Cash_Register_Divyansh.Contracts.Data;
using Cash_Register_Divyansh.Models;
using Cash_Register_Divyansh.Utility;
using System;
using System.Collections.Generic;

namespace Cash_Register_Divyansh.BusinessLogic
{
    /// <summary>
    /// Main class of the application. Responsible for implementing the cash register business logic
    /// </summary>
    public class CashRegisterManager : ICashRegisterManager
    {
        private readonly ICashRegisterRepository _cashRegRepo;

        private readonly ISummaryDisplayManager _summaryManager;
        private List<Item> _masterList;
        private List<ListItem> _inProcessItemList;

        public CashRegisterManager(ICashRegisterRepository cashRegRepo, ISummaryDisplayManager summaryManager)
        {
            _cashRegRepo = cashRegRepo;
            _summaryManager = summaryManager;
            _masterList = new List<Item>();
            _inProcessItemList = new List<ListItem>();
        }

        /// <summary>
        /// Initiate the cash register main process flow
        /// </summary>
        public async void StartProcess()
        {
            try
            {
                _masterList = await _cashRegRepo.GetItemList();
                var counterFlag = true;
                while (counterFlag)
                {
                    counterFlag = StartItemScan();
                }
                //show item summary here
                _summaryManager.ShowSummary(_inProcessItemList);
                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Error Occured: {0}. Please try again later", e.Message));
                Console.Read();
            }

        }

        /// <summary>
        /// Responsible for scanning the items
        /// </summary>
        /// <returns>The flag which updates the condition for item scanning loop</returns>
        public bool StartItemScan()
        {
            const string wrongInput = "The entered input is not valid. Please try adding the item again.";
            Console.WriteLine("Enter the name of the item");
            var input = Console.ReadLine();
            var continueToScan = CommonUtility.ContinueToScan(input);
            if (continueToScan)
            {
                var item = CommonUtility.FindItemByName(_masterList, input);
                if (item != null)
                {
                    switch (item.Type)
                    {
                        case ItemType.ByQuantity:
                            Console.WriteLine("Enter the desired quantity:");
                            if (int.TryParse(Console.ReadLine(), out var quantity))
                            {
                                _inProcessItemList = CommonUtility.AddOrUpdateInProcessItemList(_inProcessItemList, item, quantity);
                            }
                            else
                            {
                                Console.WriteLine(wrongInput);
                            }
                            break;
                        case ItemType.ByWeight:
                            Console.WriteLine("Enter the weight of item (in lb):");
                            if (decimal.TryParse(Console.ReadLine(), out var quantityInDecimal))
                            {
                                _inProcessItemList = CommonUtility.AddOrUpdateInProcessItemList(_inProcessItemList, item, quantityInDecimal);
                            }
                            else
                            {
                                Console.WriteLine(wrongInput);
                            }
                            break;
                        default:
                            Console.WriteLine("Item type not found");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Item not found");

                }
            }

            return continueToScan;
        }

    }
}
