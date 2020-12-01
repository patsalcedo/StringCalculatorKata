using System;
using Xunit;

namespace StringCalculator.Tests
{
    public class CalculatorTest
    {
        private readonly Calculator _calculator;
        
        public CalculatorTest()
        {
            _calculator = new Calculator();
        }
        
        [Fact] // step 1
        public void Return_Zero_To_Empty_String()
        {
            // WHEN
            var actual = _calculator.Add("");

            // THEN
            Assert.Equal(0,actual);
        }

        [Theory] // step 2
        [InlineData("1",1)]
        [InlineData("3",3)]
        public void Return_Number_Given_Single_String_Number(string input, int expectedOutput)
        {
            // WHEN
            var actual = _calculator.Add(input);
            
            // THEN
            Assert.Equal(expectedOutput, actual);
        }

        [Theory] // step 3
        [InlineData("1,2", 3)]
        [InlineData("3,5", 8)]
        public void Return_Sum_Of_Two_Number_Input(string input, int expectedOutput)
        {
            // WHEN
            var actual = _calculator.Add(input);

            // THEN
            Assert.Equal(expectedOutput, actual);
        }
        
        [Theory] // step 4
        [InlineData("1,2,3",6)]
        [InlineData("3,5,3,9",20)]
        public void Return_Sum_Of_Input_String_Numbers(string input, int expectedOutput)
        {
            // WHEN
            var actual = _calculator.Add(input);

            // THEN
            Assert.Equal(expectedOutput, actual);
        }
        
        [Theory] // step 5
        [InlineData("1,2\n3", 6)]
        [InlineData("3\n5\n3,9", 20)]
        public void Return_Sum_Of_Multiple_With_NewLine_And_Commas(string input, int expectedOutput)
        {
            // WHEN
            var actual = _calculator.Add(input);

            // THEN
            Assert.Equal(expectedOutput, actual);
        }
        
        [Fact] // step 6
        public void Support_Different_Delimiters()
        {
            // WHEN
            var actual = _calculator.Add("//;\n1;2");
        
            // THEN
            Assert.Equal(3, actual);
        }
        
        [Fact] // step 7
        public void Throw_Exception_For_Negatives()
        {
            var e = Assert.ThrowsAny<Exception>(() => _calculator.Add("-1,2,-3"));
            Assert.Equal("Negatives not allowed: -1, -3",e.Message);
        }

        [Fact] // step 8
        public void Ignore_Numbers_Greater_Than_Thousand()
        {
            // WHEN
            var actual = _calculator.Add("1000,1001,2");
        
            // THEN
            Assert.Equal(2, actual);
        }
        
        [Fact] // step 9
        public void Allow_Delimiters_Of_Any_Length()
        {
            // WHEN
            var actual = _calculator.Add("//[***]\n1***2***3");
        
            // THEN
            Assert.Equal(6, actual);
        }
        
        [Fact] // step 10
        public void Allow_Multiple_Delimiters()
        {
            // WHEN
            var actual = _calculator.Add("//[*][%]\n1*2%3");
        
            // THEN
            Assert.Equal(6, actual);
        }
        
        [Fact] // step 11
        public void Allow_Varying_Length_Multiple_Delimiters()
        {
            // WHEN
            var actual = _calculator.Add("//[***][#][%]\n1***2#3%4");
        
            // THEN
            Assert.Equal(10, actual);
        }
        
        [Fact] // step 12
        public void Allow_Delimiters_With_Numbers_In_Them()
        {
            // WHEN
            var actual = _calculator.Add("//[*1*][%]\n1*1*2%3");
        
            // THEN
            Assert.Equal(6, actual);
        }
    }
}