using Microsoft.VisualBasic.CompilerServices;
using NLog;
using System;
using System.Linq;


namespace ExceptionHandlingConvertToIntLibruary
{
    public static class ExHConvert
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public static bool TryToInt(string? input, out int result)
        {            
            var isParsed = false;
            result = 0;
            try
            {
                result = ToInt(input);
                isParsed = true;
            }
            catch(ArgumentNullException ex)
            {
                logger.Info(ex.Message, ex.StackTrace);
            }
            catch (FormatException ex)
            {
                logger.Info(ex.Message, ex.StackTrace);
            }
            catch (OverflowException ex)
            {
                logger.Info(ex.Message, ex.StackTrace);
            }
            return isParsed; 
        }
        public static int ToInt(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException($"{nameof(input)}");
            }
            if (input == String.Empty)
            {
                throw new FormatException($"Input {nameof(input)} is empty.");
            }
            bool isNegativeNumber = CanBeNegativeNumber(input.Trim());
            var result = 0L;
            if (!isNegativeNumber && input.Contains('-'))
            {
                throw new FormatException("Input string contain not digit symbol");
            }
            else
            {
                if (isNegativeNumber)
                {
                    input = input.Remove(0, 1);
                }
                var isContainOnlyDigits = input.All(c => char.IsDigit(c));
                if (!isContainOnlyDigits)
                {
                    throw new FormatException("Input string contain not digit symbol");
                }
                var length = input.Count();
                for (int i = 0; i < length - 1; ++i)
                {
                    result += input[i].ToInt32();
                    result *= 10;
                }
                result += input[length - 1].ToInt32();
            }
            if (!isNegativeNumber && result > int.MaxValue)
            {
                throw new OverflowException();
            }
            if (isNegativeNumber && (-1 * result) < int.MinValue)
            {
                throw new OverflowException();
            }
            return isNegativeNumber ? -1 * ((int) result) : (int) result;

        }        
        private static bool CanBeNegativeNumber(string input)
        {
            var firstIndexMinusInInput = input.IndexOf('-');
            var lastIndexMinusInInput = input.LastIndexOf('-');
            return firstIndexMinusInInput == 0 && firstIndexMinusInInput == lastIndexMinusInInput ? true : false;
        }        
        private static int ToInt32(this char c)
        {
            return (int) Char.GetNumericValue(c); // or c & 0x0f;
        }
    }
}
