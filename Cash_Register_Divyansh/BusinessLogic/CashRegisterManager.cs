using Cash_Register_Divyansh.ApplicationTypes;
using Cash_Register_Divyansh.Contracts.Business;
using Cash_Register_Divyansh.Contracts.Data;
using Cash_Register_Divyansh.Models;
using Cash_Register_Divyansh.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Cash_Register_Divyansh.BusinessLogic
{
    public class CashRegisterManager : ICashRegisterManager
    {
        private readonly IConfigurationRoot _config;
        private readonly ICashRegisterRepository _cashRegRepo;

        private readonly ISummaryDisplayManager _summaryManager;
        private List<Item> _masterList;
        private static List<ListItem> _inProcessItemList;

        public CashRegisterManager(IConfigurationRoot config, ICashRegisterRepository cashRegRepo, ISummaryDisplayManager summaryManager)
        {
            _config = config;
            _cashRegRepo = cashRegRepo;
            _summaryManager = summaryManager;
            _masterList = new List<Item>();
            _inProcessItemList = new List<ListItem>();
        }

        public bool StartItemScan()
        {
            Console.WriteLine("Enter the name of the item");
            var input = Console.ReadLine();
            var continueToScan = CommonUtility.ContinueToScan(input);
            if (continueToScan)
            {
                var item = FindItemByName(input);
                if (item != null)
                {
                    switch (item.Type)
                    {
                        case ItemType.ByQuantity:
                            Console.WriteLine("Enter the desired quantity:");
                            if (int.TryParse(Console.ReadLine(), out var quantity))
                            {
                                AddOrUpdateProcessedItemList(item, quantity);
                            }
                            break;
                        case ItemType.ByWeight:
                            Console.WriteLine("Enter the weight of item (in lb):");
                            if (decimal.TryParse(Console.ReadLine(), out var quantityInDecimal))
                            {
                                AddOrUpdateProcessedItemList(item, quantityInDecimal);
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

        public async void StartProcess(bool initScanner)
        {
            if (!initScanner) return;

            var filePath = string.Format("{0}\\{1}", Environment.CurrentDirectory,
                _config["AppSettings:ItemDefinitionFileRelativePath"]);
            _masterList = await _cashRegRepo.GetItemListFromXml(filePath);
            //Console.WriteLine("Starting Item Scan Module");
            while (initScanner)
            {
                initScanner = StartItemScan();
            }
            //show item summary here
            _summaryManager.ShowSummary(_inProcessItemList);
            Console.Read();
        }

        private Item FindItemByName(string name)
        {
            return _masterList.FirstOrDefault(it => it.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private static void AddOrUpdateProcessedItemList(Item item, decimal quantity)
        {
            var existingItem = _inProcessItemList.FirstOrDefault(x => x.Item.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
            if (existingItem != null)
            {
                //update the list item
                existingItem.EnteredQuantity += quantity;
            }
            else
            {
                _inProcessItemList.Add(new ListItem { Item = item, EnteredQuantity = quantity });
            }
        }
    }
}
