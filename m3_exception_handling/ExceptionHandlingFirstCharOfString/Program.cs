using ExceptionHandlingConvertToIntLibruary;
using ExceptionHandlingFirstCharOfString.Exceptions;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.IO;

namespace ExceptionHandlingFirstCharOfString
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args)
                .Build();
            StartWithConfig(config);
        }

        private static void StartWithConfig(IConfiguration configuration)
        {
            var stringHandler = new StringHandler();
            var commanLineKey = configuration["ReadFile"];
            if (!String.IsNullOrEmpty(commanLineKey))
            {
                try
                {
                    WorkWithFile(stringHandler, configuration);
                }
                catch (FormatException ex)
                {
                    logger.Info(ex.Message, ex.StackTrace);
                }
            }
            else
            {
                WorkWithConsole(stringHandler);
            }
        }

        private static void WorkWithConsole(StringHandler stringHandler)
        {
            var isDoneWork = false;
            while (!isDoneWork)
            {
                try
                {
                    logger.Info("Input text: ");
                    var input = Console.ReadLine();
                    logger.Info(input);
                    logger.Info($"The first symbol is: {stringHandler.GetFirstSymbolFromLine(input)}");
                    logger.Info("Exit press CTRL+C, continue press any key");
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    logger.Trace($"key: {keyInfo.Key} key char: {keyInfo.KeyChar} modifiers: {keyInfo.Modifiers}");
                }
                catch (NoElementsException ex)
                {
                    logger.Info(ex.Message, ex.TypeElements, ex.StackTrace);
                }
                catch (ArgumentNullException ex)
                {
                    logger.Info(ex.Message, ex.StackTrace);
                }
            }
        }

        private static void WorkWithFile(StringHandler stringHandler, IConfiguration configuration)
        {
            var pathFile = configuration["FileToRead:PathFile"];
            try
            {
                if (!ExHConvert.TryToInt(configuration["MaxCountReadLine"], out int maxCountReadLine))
                {
                    throw new FormatException($"The value if {nameof(maxCountReadLine)} must be int. Please check it.");
                }
                foreach (var symbol in stringHandler.GetFirstLettersOfLinesFromFile(pathFile))
                {
                    if (stringHandler.IsReadAllFile || stringHandler.CountReadLine == maxCountReadLine)
                    {
                        logger.Info("Work done.");
                        return;
                    }
                    try
                    {
                        logger.Info($"The first symbol is: {symbol}");
                    }
                    catch (ArgumentNullException ex)
                    {
                        logger.Info(ex.Message, ex.StackTrace);
                    }
                }
            }
            catch (NoElementsException ex)
            {
                logger.Info(ex.Message, ex.StackTrace);
            }
            catch (FileNotFoundException ex)
            {
                logger.Error(ex.Message, ex.StackTrace);
            }
            catch (FormatException ex)
            {
                logger.Error(ex.Message, ex.StackTrace);
            }
        }        
    }
}
