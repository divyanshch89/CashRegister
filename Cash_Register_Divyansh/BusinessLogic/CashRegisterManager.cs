using Cash_Register_Divyansh.ApplicationTypes;
using Cash_Register_Divyansh.Contracts.Business;
using Cash_Register_Divyansh.Contracts.Data;
using Cash_Register_Divyansh.Models;
using Cash_Register_Divyansh.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cash_Register_Divyansh.BusinessLogic
{
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

        public bool StartItemScan()
        {
            const string wrongInput = "The entered input is not valid. Please try adding the item again.";
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
                            else
                            {
                                Console.WriteLine(wrongInput);
                            }
                            break;
                        case ItemType.ByWeight:
                            Console.WriteLine("Enter the weight of item (in lb):");
                            if (decimal.TryParse(Console.ReadLine(), out var quantityInDecimal))
                            {
                                AddOrUpdateProcessedItemList(item, quantityInDecimal);
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

        private Item FindItemByName(string name)
        {
            return _masterList.FirstOrDefault(it => it.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private void AddOrUpdateProcessedItemList(Item item, decimal quantity)
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
