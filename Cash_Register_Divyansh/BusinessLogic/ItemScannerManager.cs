using System;
using System.Collections.Generic;
using System.Text;
using Cash_Register_Divyansh.ApplicationTypes;
using Cash_Register_Divyansh.Contracts.Business;
using Cash_Register_Divyansh.Models;

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
                    var baseDiscountedQty = Convert.ToInt16(scannedItem.DiscountValue.Split('|')[0]);
                    var discountedQty = Convert.ToInt16(scannedItem.DiscountValue.Split('|')[1]);
                    var calculatedDiscountedQty = ((int)quantity / baseDiscountedQty) * discountedQty;
                    lItem.CalculatedCost = scannedItem.PerUnitPrice * (quantity - calculatedDiscountedQty);
                    lItem.Item = scannedItem;
                    if (calculatedDiscountedQty > 0)
                    {
                        lItem.DiscountSummary = string.Format("Discount Applied: Buy {0} get {1} free", baseDiscountedQty, discountedQty);
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
