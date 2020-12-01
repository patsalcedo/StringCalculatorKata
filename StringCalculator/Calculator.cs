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
            string[] defaultDelimiterChars = {",", "\n"};

            var arrayFromString = HasCustomDelimiters(input)
                ? GetArrayFromInputWithCustomDelimiters(input)
                : CreateIntArrayFromInput(input, defaultDelimiterChars);
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
                ThrowsNegativeNumberException(negNumList);
            }
            validNumbers.RemoveAll(n => n >= 1000);
            return validNumbers;
        }

        private static void ThrowsNegativeNumberException(string input)
        {
            throw new ArithmeticException("Negatives not allowed: " + input);
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