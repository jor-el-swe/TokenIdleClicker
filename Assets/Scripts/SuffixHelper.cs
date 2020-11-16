using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SuffixHelper
{
    public static string GetString( ulong num )
    {
        ulong numStr;
        string suffix;
        if( num < 1000ul )
        {
            numStr = num;
            suffix = "";
        }
        else if( num < 1000000ul )
        {
            numStr = num/1000ul;
            suffix = " Thousands";
        }
        else if( num < 1000000000ul )
        {
            numStr = num/1000000ul;
            suffix = " Millions";
        }
        else if( num < 1000000000000ul )
        {
            numStr = num/1000000000ul;
            suffix = " Billions";
        }
        else
        {
            numStr = num/1000000000000ul;
            suffix = " Trillions";
        }

        return numStr.ToString() + suffix;
    }
}
