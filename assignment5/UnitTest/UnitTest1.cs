using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderApp;

namespace OrderApp.Tests
{
    [TestClass]
    public class OrderItemTests
    {
        [TestMethod]
        public void Total_ShouldCalculateAmountTimesPrice()
        {
            var item = new OrderItem { Amount = 3, Price = 10 };
            Assert.AreEqual(30, item.Total);
        }

        [TestMethod]
        public void Equals_ShouldReturnTrueForSameProduct()
        {
            var item1 = new OrderItem { Product = "Apple" };
            var item2 = new OrderItem { Product = "Apple" };
            Assert.IsTrue(item1.Equals(item2));
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            var item = new OrderItem { Product = "Apple", Amount = 2, Price = 5 };
            string expected = "Apple ×2 ¥5 (小计:¥10)";
            Assert.AreEqual(expected, item.ToString());
        }
    }
}


namespace OrderApp.Tests
{
    [TestClass]
    public class OrderManagerTests
    {
        private OrderManager manager;

        [TestInitialize]
        public void Setup()
        {
            manager = new OrderManager();
        }

        [TestMethod]
        public void AddOrder_ShouldAddSuccessfully()
        {
            var order = new Order { ID = 1, Customer = "Test" };
            manager.Add(order);
            var found = manager.Find(o => o.ID == 1);
            Assert.AreEqual(1, found.Count);
            Assert.AreEqual("Test", found[0].Customer);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddDuplicateOrder_ShouldThrowException()
        {
            var order1 = new Order { ID = 1 };
            var order2 = new Order { ID = 1 };
            manager.Add(order1);
            manager.Add(order2);
        }

        [TestMethod]
        public void DeleteOrder_ShouldRemoveOrder()
        {
            var order = new Order { ID = 1 };
            manager.Add(order);
            manager.Delete(1);
            var found = manager.Find(o => o.ID == 1);
            Assert.AreEqual(0, found.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteNonExistentOrder_ShouldThrowException()
        {
            manager.Delete(999);
        }

        [TestMethod]
        public void UpdateOrder_ShouldModifyCustomer()
        {
            var original = new Order { ID = 1, Customer = "Old" };
            manager.Add(original);
            var updated = new Order { ID = 1, Customer = "New" };
            manager.Update(updated);
            var found = manager.Find(o => o.ID == 1);
            Assert.AreEqual("New", found[0].Customer);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateNonExistentOrder_ShouldThrowException()
        {
            var order = new Order { ID = 1 };
            manager.Update(order);
        }

        [TestMethod]
        public void FindOrdersByProduct_ShouldReturnCorrectOrders()
        {
            var order1 = new Order { ID = 1 };
            order1.Items.Add(new OrderItem { Product = "Apple" });
            var order2 = new Order { ID = 2 };
            order2.Items.Add(new OrderItem { Product = "Banana" });
            manager.Add(order1);
            manager.Add(order2);

            var result = manager.Find(o => o.Items.Any(i => i.Product == "Apple"));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].ID);
        }

        [TestMethod]
        public void SortOrdersById_ShouldOrderCorrectly()
        {
            manager.Add(new Order { ID = 3 });
            manager.Add(new Order { ID = 1 });
            manager.Add(new Order { ID = 2 });
            manager.Sort();
            var all = manager.Find(o => true);
            Assert.AreEqual(1, all[0].ID);
            Assert.AreEqual(2, all[1].ID);
            Assert.AreEqual(3, all[2].ID);
        }

    }
}