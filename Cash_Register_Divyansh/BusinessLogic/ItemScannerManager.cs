using Cash_Register_Divyansh.ApplicationTypes;
using Cash_Register_Divyansh.Contracts.Business;
using Cash_Register_Divyansh.Models;
using System;

namespace Cash_Register_Divyansh.BusinessLogic
{
    public class ItemScannerManager : IItemScannerManager
    {

        public ListItem ScanAndCalculateCost(Item scannedItem, decimal quantity)
        {
            var lItem = new ListItem();
            
                switch (scannedItem.DiscountType)
                {
                    case DiscountType.Cost:
                        lItem.CalculatedCost = (100 - Convert.ToDecimal(scannedItem.DiscountValue)) * (decimal)0.01 *
                                               scannedItem.PerUnitPrice * quantity;
                        lItem.Item = scannedItem;
                        lItem.DiscountSummary = string.Format("Discount Applied: {0}% off", scannedItem.DiscountValue);
                        break;
                    case DiscountType.Quantity:
                        var baseQty = Convert.ToInt16(scannedItem.DiscountValue.Split('|')[0]);
                        var discountedQty = Convert.ToInt16(scannedItem.DiscountValue.Split('|')[1]);
                        var qtyPerDiscountedQty = (int)(quantity / (baseQty + discountedQty));
                        lItem.CalculatedCost = qtyPerDiscountedQty * baseQty *
                                               scannedItem.PerUnitPrice + ((quantity % (baseQty + discountedQty)) * scannedItem.PerUnitPrice);
                        lItem.Item = scannedItem;
                        if (qtyPerDiscountedQty > 0)
                        {
                            lItem.DiscountSummary = string.Format("Discount Applied: Buy {0} get {1} free", baseQty, discountedQty);
                        }
                        break;
                    default:
                        lItem.CalculatedCost = scannedItem.PerUnitPrice * quantity;
                        lItem.Item = scannedItem;
                        break;
                }
                lItem.EnteredQuantity = quantity;
            
           
            return lItem;
        }
    }
}
