namespace Cash_Register_Divyansh.Models
{
    public class ListItem
    {
        public Item Item { get; set; }
        public decimal CalculatedCost { get; set; }
        public decimal EnteredQuantity { get; set; }
        public string DiscountSummary { get; set; }
    }
}
