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
            var validNumbers = FindValidNumbers(ParseInputIntoArray(input)); // was arrayFromString
            return validNumbers.Sum();
        }

        private int[] ParseInputIntoArray(string input)
        {
            var arrayFromString = HasCustomDelimiters(input)
                ? GetArrayFromInputWithCustomDelimiters(input)
                : GetArrayFromInputWithDefaultDelimiters(input);
            return arrayFromString;
        }

        private bool HasCustomDelimiters(string input)
        {
            return input.StartsWith("//");
        }

        private static int[] GetArrayFromInputWithCustomDelimiters(string input)
        {
            var inputHalf = input.Split('\n'); 
            var delimiters = inputHalf[0].Split(new[] {'[', ']', '/'}, StringSplitOptions.RemoveEmptyEntries);
            return CreateIntArrayFromInput(inputHalf[1], delimiters);
        }
        
        private static int[] GetArrayFromInputWithDefaultDelimiters(string input)
        {
            string[] defaultDelimiterChars = {",", "\n"};
            return CreateIntArrayFromInput(input, defaultDelimiterChars);
        }

        private static List<int> FindValidNumbers(int[] arrayFromString)
        {
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
            return validNumbers;
        }

        private static int[] CreateIntArrayFromInput(string data, string[] delimiters)
        {
            var arrayFromString = data
                .Split(delimiters, StringSplitOptions.None)
                .Select(n => Convert.ToInt32(n))
                .ToArray();
            return arrayFromString;
        }
    }
}