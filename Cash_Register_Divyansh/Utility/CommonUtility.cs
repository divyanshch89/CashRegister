using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Cash_Register_Divyansh.Models;

namespace Cash_Register_Divyansh.Utility
{
    public class CommonUtility
    {
        public static bool ContinueToScan(string input)
        {
            return !input.Equals("Exit", StringComparison.OrdinalIgnoreCase);
        }
    }
}
