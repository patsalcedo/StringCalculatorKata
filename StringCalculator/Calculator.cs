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
            if (int.TryParse(input, out var numberForTryParse))
            {
                if (numberForTryParse < 0)
                {
                    throw new ArithmeticException("Negatives not allowed: " + input);
                }
                return numberForTryParse;
            }

            int[] arrayFromString;
            char[] delimiterChars = {',','\n'};
            if (input.StartsWith("//"))
            {
                var subInput = "";
                var delimiter = "";
                var firstBracket = input.IndexOf("[", StringComparison.Ordinal);
                if (firstBracket == -1)
                {
                    subInput = input.Substring(4);
                    delimiter = input.Substring(2, 1);
                }
                else
                {
                    var data = input.ToList();
                    if (data.Count(x => x == '[') > 1)
                    {
                        // I figured out you can split by any and multiple specified characters so I just did it in one go
                        var resultOfBracketAndSlashSplit = input.Split('[',']','/'); // = *%\n1*2%3
                        var resultOfSplitToStr = string.Join("", resultOfBracketAndSlashSplit); // makes into string
                        var splitDelimiterFromData = resultOfSplitToStr.Split('\n'); // *% and 1*2%3
                        var delimiterAsString = splitDelimiterFromData[0]; // gets only *%
                        var dataStartIndex = input.IndexOf("\n", StringComparison.Ordinal);
                        subInput = input.Substring(dataStartIndex); // 1*2%3
                        // subInput = splitDelimiterFromData[1];
                        delimiterChars = delimiterAsString.ToCharArray();
                        arrayFromString = subInput.Split(delimiterChars,StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).ToArray();
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
                arrayFromString = subInput.Split(delimiter).Select(n => Convert.ToInt32(n)).ToArray();
            }
            else
            {
                arrayFromString = input.Split(delimiterChars).Select(n => Convert.ToInt32(n)).ToArray();
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