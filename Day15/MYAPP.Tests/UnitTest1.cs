using Xunit;
using MYAPP;

namespace MYAPP.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_Test()
        {
            var calc = new Calculator();
            Assert.Equal(15, calc.Add(5, 10));
        }

        [Fact]
        public void Subtract_Test()
        {
            var calc = new Calculator();
            Assert.Equal(5, calc.Subtract(10, 5));
        }

        [Fact]
        public void Multiply_Test()
        {
            var calc = new Calculator();
            Assert.Equal(20, calc.Multiply(4, 5));
        }
    }
}