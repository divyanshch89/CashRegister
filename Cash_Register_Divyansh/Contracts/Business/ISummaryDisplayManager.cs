using System;
using System.Collections.Generic;
using System.Text;
using Cash_Register_Divyansh.Models;

namespace Cash_Register_Divyansh.Contracts.Business
{
    public interface ISummaryDisplayManager
    {
        void ShowSummary(List<ListItem> itemList);
    }
}
