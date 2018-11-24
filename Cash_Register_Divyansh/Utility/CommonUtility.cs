using System;

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
