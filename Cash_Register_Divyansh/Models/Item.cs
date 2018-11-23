using System;
using System.Xml.Serialization;
using Cash_Register_Divyansh.ApplicationTypes;

namespace Cash_Register_Divyansh.Models
{
    [Serializable, XmlRoot("Items")]
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public Decimal PerUnitPrice { get; set; }
        public DiscountType DiscountType { get; set; }
        public string DiscountValue { get; set; }
    }
}
