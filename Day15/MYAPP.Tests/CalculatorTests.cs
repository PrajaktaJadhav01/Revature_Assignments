using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYAPP;

namespace MYAPP.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        Calculator calc = new Calculator();

        [TestMethod]
        public void Add_Test1()
        {
            Assert.AreEqual(15, calc.Add(5, 10));
        }

        [TestMethod]
        public void Add_Test2()
        {
            Assert.AreEqual(20, calc.Add(10, 10));
        }

        [TestMethod]
        public void Add_Test3()
        {
            Assert.AreEqual(0, calc.Add(-5, 5));
        }

        [TestMethod]
        public void Subtract_Test1()
        {
            Assert.AreEqual(5, calc.Subtract(10, 5));
        }

        [TestMethod]
        public void Subtract_Test2()
        {
            Assert.AreEqual(0, calc.Subtract(5, 5));
        }

        [TestMethod]
        public void Subtract_Test3()
        {
            Assert.AreEqual(-5, calc.Subtract(5, 10));
        }

        [TestMethod]
        public void Multiply_Test1()
        {
            Assert.AreEqual(20, calc.Multiply(4, 5));
        }

        [TestMethod]
        public void Multiply_Test2()
        {
            Assert.AreEqual(0, calc.Multiply(0, 5));
        }

        [TestMethod]
        public void Multiply_Test3()
        {
            Assert.AreEqual(-20, calc.Multiply(-4, 5));
        }

        [TestMethod]
        public void Divide_Test1()
        {
            Assert.AreEqual(5, calc.Divide(20, 4));
        }

        [TestMethod]
        public void Divide_Test2()
        {
            Assert.AreEqual(2, calc.Divide(10, 5));
        }

        [TestMethod]
        public void Divide_Test3()
        {
            Assert.AreEqual(1, calc.Divide(5, 5));
        }
    }
}