using Cash_Register_Divyansh.Models;
using System.Collections.Generic;

namespace Cash_Register_Divyansh.Contracts.Business
{
    public interface ISummaryDisplayManager
    {
        void ShowSummary(List<ListItem> itemList);
    }
}
