using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


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
                if (int.Parse(input) < 0)
                {
                    throw new ArithmeticException("Negatives not allowed: " + input);
                }
                return int.Parse(input);
            }

            int[] arrayFromString;
            if (input.StartsWith("//"))
            {
                List<char> inputAsCharList = new List<char>();
                inputAsCharList.AddRange(input);
                var firstBracket = input.IndexOf("[");
                
                if (firstBracket == -1)
                {
                    var subInput = input.Substring(4);
                    var delimiter = input.Substring(2, 1);
                    arrayFromString = subInput.Split(delimiter).Select(n => Convert.ToInt32(n)).ToArray();
                }
                else
                {
                     var secondBracket = input.IndexOf("]");
                     var delimiter = input.Substring(firstBracket+1, (secondBracket - firstBracket -1));
                    
                     var dataStartIndex = input.IndexOf("\n");
                     var subInput = input.Substring(dataStartIndex);
                                    
                     arrayFromString = subInput.Split(delimiter).Select(n => Convert.ToInt32(n)).ToArray();
                }
               
            }
            else
            {
                char[] delimiterChars = {',','\n'};
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