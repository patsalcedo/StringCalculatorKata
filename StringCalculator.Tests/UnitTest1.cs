using System;
using Xunit;

namespace StringCalculator.Tests
{
    public class UnitTest1
    {
        // private readonly Calculator _calculator;
        //
        // public CalculatorTest
        // {
        //     
        // }
        
        [Fact]
        public void Return_Zero_To_Empty_String()
        {
            // GIVEN
            var calculator = new Calculator();

            // WHEN
            var actual = calculator.Add("");

            // THEN
            Assert.Equal(0,actual);
        }

        [Theory]
        [InlineData("1",1)]
        [InlineData("3",3)]
        public void Return_Number_Given_Single_String_Number(string input, int expectedOutput)
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add(input);
            
            // THEN
            Assert.Equal(expectedOutput, actual);
        }

        [Theory]
        [InlineData("1,2,3",6)]
        [InlineData("3,5,3,9",20)]
        public void Return_Sum_Of_Input_String_Numbers(string input, int expectedOutput)
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add(input);

            // THEN
            Assert.Equal(expectedOutput, actual);
        }

        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("3,5", 8)]
        public void Return_Sum_Of_Multiple_Input(string input, int expectedOutput)
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add(input);

            // THEN
            Assert.Equal(expectedOutput, actual);
        }
        
        [Theory]
        [InlineData("1,2\n3", 6)]
        [InlineData("3\n5\n3,9", 20)]
        public void Return_Sum_Of_Multiple_With_NewLine_And_Commas(string input, int expectedOutput)
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add(input);

            // THEN
            Assert.Equal(expectedOutput, actual);
        }
        
        [Fact]
        public void Support_Different_Delimiters()
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add("//;\n1;2");
        
            // THEN
            Assert.Equal(3, actual);
        }
        
        [Fact]
        public void Throw_Exception_For_Negatives()
        {
            // GIVEN
            var calculator = new Calculator();
            
            // THEN
            var ex = Assert.ThrowsAny<Exception>(() => calculator.Add("-1,2,-3"));
            Assert.Equal("Negatives not allowed: -1, -3",ex.Message);
        }

        [Fact]
        public void Ignore_Numbers_Greater_Than_Thousand()
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add("1000,1001,2");
        
            // THEN
            // var ex = Assert.Throws<ArithmeticException>(() => actual.)
            // Assert.Equal(3, actual);
            Assert.Equal(2, actual);
        }
        
        [Fact]
        public void Allow_Delimiters_Of_Any_Length()
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add("//[***]\n1***2***3");
        
            // THEN
            Assert.Equal(6, actual);
        }
        
        [Fact]
        public void Allow_Multiple_Delimiters()
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add("//[*][%]\n1*2%3");
        
            // THEN
            Assert.Equal(6, actual);
        }
        
        [Fact]
        public void Allow_Varying_Length_Multiple_Delimiters()
        {
            // GIVEN
            var calculator = new Calculator();
            
            // WHEN
            var actual = calculator.Add("//[***][#][%]\n1***2#3%4");
        
            // THEN
            Assert.Equal(10, actual);
        }
    }
}