using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYAPP;

namespace MYAPP.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void OrderEmail_Test()
        {
            Order order = new Order { Email = "test@gmail.com" };

            Assert.AreEqual("test@gmail.com", order.Email);
        }

        [TestMethod]
        public void OrderEmail_NotNull_Test()
        {
            Order order = new Order { Email = "abc@gmail.com" };

            Assert.IsNotNull(order.Email);
        }
    }
}