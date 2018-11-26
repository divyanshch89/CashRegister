using Cash_Register_Divyansh.Models;

namespace Cash_Register_Divyansh.Contracts.Business
{
    public interface ICostCalculatorManager
    {
        ListItem CalculateCost(Item scannedItem, decimal quantity);
    }
}
