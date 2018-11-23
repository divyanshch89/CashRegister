using Cash_Register_Divyansh.Contracts.Business;
using Cash_Register_Divyansh.Models;
using System;
using System.Collections.Generic;

namespace Cash_Register_Divyansh.BusinessLogic
{
    public class SummaryDisplayManager : ISummaryDisplayManager
    {
        private readonly IItemScannerManager _itemScanManager;
        public SummaryDisplayManager(IItemScannerManager itemScanManager)
        {
            _itemScanManager = itemScanManager;
        }
        public void ShowSummary(List<ListItem> itemList)
        {
            decimal totalCost = 0;
            Console.WriteLine(string.Empty);
            if (itemList.Count <= 0)
            {
                Console.WriteLine("No price summary to display");
                return;
            }
            Console.WriteLine("\t\t***Item Summary\n***\t\t");
            foreach (var item in itemList)
            {
                //price calculation
                var processedItem = _itemScanManager.ScanAndCalculateCost(item.Item, item.EnteredQuantity);
                Console.WriteLine("Item Name: {0} \t Quantity/Weight (in lb): {1} \t Unit Cost: ${3} \t Item Cost: ${2} \t {4} \t",
                    processedItem.Item.Name, processedItem.EnteredQuantity, processedItem.CalculatedCost, processedItem.Item.PerUnitPrice, processedItem.DiscountSummary);
                totalCost += processedItem.CalculatedCost;
            }
            Console.WriteLine("\nTotal Amount Due: ${0}", totalCost);
        }
    }
}
