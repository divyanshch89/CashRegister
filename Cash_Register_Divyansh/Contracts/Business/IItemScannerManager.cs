using Cash_Register_Divyansh.Models;

namespace Cash_Register_Divyansh.Contracts.Business
{
    public interface IItemScannerManager
    {
        ListItem ScanAndCalculateCost(Item scannedItem, decimal quantity);
    }
}
