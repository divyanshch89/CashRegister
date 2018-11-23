using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cash_Register_Divyansh.Models;

namespace Cash_Register_Divyansh.Contracts.Data
{
    public interface ICashRegisterRepository
    {
        Task<List<Item>> GetItemListFromXml(string itemDefXmlPath);
    }
}
