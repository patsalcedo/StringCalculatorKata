using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
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
                    throw new ArithmeticException("Negatives not allowed: " + input);
                }
                return inputAsNumber;
            }

            int[] arrayFromString;
            char[] defaultDelimiterChars = {',','\n'};
            if (input.StartsWith("//"))
            {
                var subInput = "";
                var delimiter = "";
                var firstBracket = input.IndexOf("[", StringComparison.Ordinal);
                if (firstBracket == -1) // no square brackets
                {
                    subInput = input.Substring(4);
                    delimiter = input.Substring(2, 1);
                    arrayFromString = subInput.Split(delimiter).Select(n => Convert.ToInt32(n)).ToArray();
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
                        // var dataStartIndex = input.IndexOf("\n", StringComparison.Ordinal);
                        // subInput = input.Substring(dataStartIndex); // 1*2%3
                        // subInput = splitDelimiterFromData[1];
                        // defaultDelimiterChars = delimiterAsString.ToCharArray();
                        arrayFromString = splitDelimiterFromData[1]
                            .Split(delimiterAsString.ToCharArray(),StringSplitOptions.RemoveEmptyEntries)
                            .Select(n => Convert.ToInt32(n)).ToArray();
                    }
                    else
                    {
                        var secondBracket = input.IndexOf("]", StringComparison.Ordinal);
                        delimiter = input.Substring(firstBracket+1, (secondBracket - firstBracket -1));
                        var dataStartIndex = input.IndexOf("\n", StringComparison.Ordinal);
                        subInput = input.Substring(dataStartIndex);
                        arrayFromString = subInput.Split(delimiter).Select(n => Convert.ToInt32(n)).ToArray();
                    }
                }
                // arrayFromString = subInput.Split(delimiter).Select(n => Convert.ToInt32(n)).ToArray();
            }
            else
            {
                arrayFromString = input.Split(defaultDelimiterChars).Select(n => Convert.ToInt32(n)).ToArray();
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
    }
}