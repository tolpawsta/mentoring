using Microsoft.VisualBasic.CompilerServices;
using System;

namespace ExceptionHandlingConvertToIntLibruary
{
    public static class ExHConvert
    {
        public static int ToInt(string input)
        {
            int result;            
                result = int.Parse(input);            
            return result;

        }
        public static bool TryToInt(string? input, out int result)
        {
            result = 0;
            return true;
        }
    }
}
