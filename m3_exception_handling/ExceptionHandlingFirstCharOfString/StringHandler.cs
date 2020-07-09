using ExceptionHandlingFirstCharOfString.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExceptionHandlingFirstCharOfString
{
    public class StringHandler
    {
        public bool IsReadAllFile { get; private set; }
        public int CountReadLine { get; private set; }

        public IEnumerable<char> GetFirstLettersOfLinesFromFile(string pathFile)
        {
            if (!File.Exists(pathFile))
            {
                throw new FileNotFoundException($"File {pathFile} not founD");
            }
            IsReadAllFile = false;
            CountReadLine = 0;
            using (StreamReader reader = new StreamReader(pathFile))
            {
                string readLine = reader.ReadLine();
                if (readLine is null)
                {                    
                    throw new NoElementsException("File is empty");
                }
                yield return GetFirstSymbolFromLine(readLine);
                while (!reader.EndOfStream)
                {
                    ++CountReadLine;
                    yield return GetFirstSymbolFromLine(reader.ReadLine());
                }                
            }
        }
        public char GetFirstSymbolFromLine(string input)
        {
            if (input.All(c =>char.IsWhiteSpace(c)))
            {
                throw new NoElementsException("Input string line consist only whitespace(s)");
            }
            if (input is null)
            {
                throw new ArgumentNullException("Input line is empty");
            }            
            return input.Trim().First();
        }
    }
}