using Cash_Register_Divyansh.ApplicationTypes;
using Cash_Register_Divyansh.Contracts.Data;
using Cash_Register_Divyansh.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Configuration;

namespace Cash_Register_Divyansh.Repositories
{

    public class CashRegisterRepository : ICashRegisterRepository
    {
        private readonly IConfigurationRoot _config;

        public CashRegisterRepository(IConfigurationRoot config)
        {
            _config = config;
        }

        /// <summary>
        /// Load master item list by reading the ItemDefinition xml located in AppData folder
        /// </summary>
        public Task<List<Item>> GetItemList()
        {
            var xmlFilePath = string.Format("{0}\\{1}", Environment.CurrentDirectory,
                _config["AppSettings:ItemDefinitionFileRelativePath"]);
            var xml = new XmlDocument();
            var masterList = new List<Item>();
            xml.Load(xmlFilePath);
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
