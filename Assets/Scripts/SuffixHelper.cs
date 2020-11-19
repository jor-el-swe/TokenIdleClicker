public static class SuffixHelper
{
    public static string GetString( ulong num, bool newLineSuffix)
    { 
        const ulong pow10_3 = 1000ul; 
        const ulong pow10_6 = 1000000ul; 
        const ulong pow10_9 = 1000000000ul; 
        const ulong pow10_12 = 1000000000000ul;
    
        var displayDecimals = true; 
        var decimals = "";
        ulong numStr;
        string suffix = "";
        
        if (newLineSuffix) {
            suffix += "\n";
        }
        
        if( num < pow10_3 )
        {
            numStr = num;
            suffix = "";
            displayDecimals = false;
        }
        else if( num < pow10_6 )
        {
            numStr = num/pow10_3;
            decimals = ModToDecimalString(num, pow10_3, ref displayDecimals);
            suffix += " K";
        }
        else if( num < pow10_9 )
        {
            numStr = num/pow10_6;
            decimals = ModToDecimalString(num, pow10_6, ref displayDecimals);
            suffix += " Millions";
        }
        else if( num < pow10_12)
        {
            numStr = num/pow10_9;
            decimals = ModToDecimalString(num, pow10_9, ref displayDecimals);
            suffix += " Billions";
        }
        else
        {
            numStr = num/pow10_12;
            decimals = ModToDecimalString(num, pow10_12, ref displayDecimals);
            suffix += " Trillions";
        }

        //make sure decimals are not too short string
        decimals += "00";
        
        if (displayDecimals)
            return numStr.ToString() + "." + decimals.Substring(0,2) + suffix;
        return numStr.ToString() + suffix;
    }

    private static string ModToDecimalString(ulong num, ulong power, ref bool dispDecimal)
    {
        var mod = num % power;
        if (mod != 0)
        {
            if((mod*100)/power!=0)
                return mod.ToString(); 
        }
        dispDecimal = false;
        return "";
    }
}