using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SuffixHelper
{
    public static string GetString( ulong num )
    {
        ulong numStr;
        string suffix;
        string decimals = "";
        if( num < 1000ul )
        {
            numStr = num;
            suffix = "";
        }
        else if( num < 1000000ul )
        {
            numStr = num/1000ul;
            decimals = (num % 1000ul).ToString();
            suffix = " K";
        }
        else if( num < 1000000000ul )
        {
            numStr = num/1000000ul;
            decimals = (num % 1000000ul).ToString();
            suffix = " Millions";
        }
        else if( num < 1000000000000ul )
        {
            numStr = num/1000000000ul;
            decimals = (num % 1000000000ul).ToString();
            suffix = " Billions";
        }
        else
        {
            numStr = num/1000000000000ul;
            decimals = (num % 1000000000000ul).ToString();
            suffix = " Trillions";
        }
        decimals += "000";
        return numStr.ToString() + "." + decimals.Substring(0,2) + suffix;
    }
}
