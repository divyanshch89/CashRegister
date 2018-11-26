using System;
using System.Collections.Generic;
using System.Linq;
using Cash_Register_Divyansh.Models;

namespace Cash_Register_Divyansh.Utility
{
    /// <summary>
    /// Contains static utility methods to be used in the application
    /// </summary>
    public class CommonUtility
    {
        /// <summary>
        /// Look for Exit input to stop the item scanning process
        /// </summary>
        /// <param name="input"></param>
        /// <returns> True when input is not equal to 'Exit'</returns>
        public static bool ContinueToScan(string input)
        {
            return !input.Equals("Exit", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Finds the item from the master items list based on Name item property
        /// </summary>
        /// <param name="masterItemList"></param>
        /// <param name="itemName"></param>
        /// <returns>matched or default item</returns>
        public static Item FindItemByName(List<Item> masterItemList, string itemName)
        {
            return masterItemList.FirstOrDefault(it => it.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Add or update items in processing list. This processing list will be used for final price calculation
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <returns> the updated in process item list</returns>
        public static List<ListItem> AddOrUpdateInProcessItemList(List<ListItem> itemList, Item item, decimal quantity)
        {
            var existingItem = itemList.FirstOrDefault(x => x.Item.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
            if (existingItem != null)
            {
                //update the list item
                existingItem.EnteredQuantity += quantity;
            }
            else
            {
                itemList.Add(new ListItem { Item = item, EnteredQuantity = quantity });
            }

            return itemList;
        }
    }
}
