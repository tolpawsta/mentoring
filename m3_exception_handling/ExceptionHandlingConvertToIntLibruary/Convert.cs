using Microsoft.VisualBasic.CompilerServices;
using System;

namespace ExceptionHandlingConvertToIntLibruary
{
    public static class Convert
    {
        public static int ToInt(string input)
        {
            return 2;
        }
        public static bool TryToInt(string? input,out int result)
        {
            result = 0;
            return true;
        }
    }
}
