using Cash_Register_Divyansh.ApplicationTypes;
using Cash_Register_Divyansh.Contracts.Data;
using Cash_Register_Divyansh.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Cash_Register_Divyansh.Repositories
{
    public class CashRegisterRepository : ICashRegisterRepository
    {
        public Task<List<Item>> GetItemListFromXml(string itemDefXmlPath)
        {
            var xml = new XmlDocument();
            var masterList = new List<Item>();
            xml.Load(itemDefXmlPath);
            foreach (XmlNode child in xml.SelectSingleNode("//items").ChildNodes)
            {
                var item = new Item
                {
                    Name = child.Attributes["name"]?.Value,
                    Type = child.Attributes["type"].Value.Equals("ByWeight", StringComparison.OrdinalIgnoreCase)
                        ? ItemType.ByWeight
                        : ItemType.ByQuantity,
                    PerUnitPrice = Convert.ToDecimal(child.Attributes["perunnitprice"]?.Value)
                };
                if (!string.IsNullOrEmpty(child.Attributes["discounttype"]?.Value))
                {
                    item.DiscountType = child.Attributes["discounttype"].Value
                        .Equals("cost", StringComparison.OrdinalIgnoreCase)
                        ? DiscountType.Cost
                        : DiscountType.Quantity;
                    item.DiscountValue = child.Attributes["discountvalue"]?.Value;
                }

                masterList.Add(item);
            }

            return Task.FromResult(masterList);
        }
    }
}
