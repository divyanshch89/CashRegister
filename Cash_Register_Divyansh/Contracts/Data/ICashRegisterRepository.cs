using Cash_Register_Divyansh.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cash_Register_Divyansh.Contracts.Data
{
    public interface ICashRegisterRepository
    {
        Task<List<Item>> GetItemListFromXml(string itemDefXmlPath);
    }
}
