using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        private const string CustomDelimiterSignifier = "//";
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            if (int.TryParse(input, out var inputAsNumber))
            {
                if (inputAsNumber < 0)
                {
                    ThrowsNegativeNumberException(input);
                }
                return inputAsNumber;
            }

            int[] arrayFromString;
            char[] defaultDelimiterChars = {',','\n'};
            if (input.StartsWith(CustomDelimiterSignifier))
            {
                var delimitersAndNumbersAsStrings = "";
                var delimiter = "";
                var firstSquareBracket = input.IndexOf("[", StringComparison.Ordinal);
                if (firstSquareBracket == -1) // no square brackets
                {
                    delimitersAndNumbersAsStrings = input.Substring(4); // = //;\n1;2
                    delimiter = input.Substring(2, 1);
                    arrayFromString = GetIntArrayFromString(delimitersAndNumbersAsStrings, delimiter);
                }
                else
                {
                    var data = input.ToList();
                    if (data.Count(x => x == '[') > 1) // if there is more than one delimiter
                    {
                        var resultOfBracketAndSlashSplit = input.Split('[',']','/');
                        var resultOfSplitToStr = string.Join("", resultOfBracketAndSlashSplit);
                        var splitDelimiterFromData = resultOfSplitToStr.Split('\n');
                        var delimiterAsString = splitDelimiterFromData[0];
                        delimitersAndNumbersAsStrings = splitDelimiterFromData[1];
                        
                        // arrayFromString = splitDelimiterFromData[1]
                        //     .Split(delimiterAsString.ToCharArray(),StringSplitOptions.RemoveEmptyEntries)
                        //     .Select(n => Convert.ToInt32(n)).ToArray();
                        arrayFromString = GetIntArrayFromCharArrayDelimiter(delimitersAndNumbersAsStrings, delimiterAsString.ToCharArray());
                    }
                    else
                    {
                        var secondBracket = input.IndexOf("]", StringComparison.Ordinal);
                        delimiter = input.Substring(firstSquareBracket+1, (secondBracket - firstSquareBracket -1));
                        var dataStartIndex = input.IndexOf("\n", StringComparison.Ordinal);
                        delimitersAndNumbersAsStrings = input.Substring(dataStartIndex);
                        arrayFromString = GetIntArrayFromString(delimitersAndNumbersAsStrings, delimiter);
                    }
                }
            }
            else
            {
                arrayFromString = GetIntArrayFromCharArrayDelimiter(input, defaultDelimiterChars);
            }
            var negativeNumbers = new List<int>();
            var validNumbers = new List<int>();
            foreach (var num in arrayFromString)
            {
                if (num < 0)
                {
                    negativeNumbers.Add(num);
                }
                else
                {
                    validNumbers.Add(num);
                }
            }
            if (negativeNumbers.Any())
            {
                var negNumList = string.Join(", ", negativeNumbers);
                throw new ArithmeticException("Negatives not allowed: " + negNumList);
            }

            validNumbers.RemoveAll(n => n >= 1000);
            return validNumbers.Sum();
        }

        private static int[] GetIntArrayFromCharArrayDelimiter(string input, char[] defaultDelimiterChars)
        {
            return input.Split(defaultDelimiterChars).Select(n => Convert.ToInt32(n)).ToArray();
        }

        private static int[] GetIntArrayFromString(string delimitersAndNumbersAsStrings, string delimiter)
        {
            return delimitersAndNumbersAsStrings.Split(delimiter).Select(n => Convert.ToInt32(n)).ToArray();
        }

        private static void ThrowsNegativeNumberException(string input)
        {
            throw new ArithmeticException("Negatives not allowed: " + input);
        }
    }
}