using MongoDB.Driver.Builders;
using MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities;
using NUnit.Framework;

namespace MongoDB.Driver.Extensions.Mapping.Tests
{
    [TestFixture]
    public class BasicPersistenceAndRetrievalTests : BaseUnitTest
    {
        private const string CustomerName = "Kumar Sangakkara";

        [SetUp]
        public void Setup()
        {
            var customer = new Customer()
                               {
                                   Name = CustomerName,
                                   CreatedBy = OrderManager
                               };
            Save(customer);

            var order = new Order()
                            {
                                CustomerId = customer.Id,
                                CreatedBy = OrderManager
                            };

            for (int i = 0; i <= 99; i++)
            {
                var line = new OrderLine()
                {
                    ProductId = (i + 1),
                    Quantity = (i + 3)
                };
                order.Lines.Add(line);
            }
            Save(order);

            customer.Orders.Add(order);
            Save(customer);
        }

        [Test]
        public void Verify_customer_has_one_order_and_it_is_loaded_correctly()
        {
            Customer customer = GetCollection<Customer>().FindOne(Query.EQ("Name", CustomerName));
            Assert.IsNotNull(customer);
            Assert.AreEqual(CustomerName, customer.Name);
            Assert.AreEqual(1, customer.Orders.Count);
            Assert.AreEqual(customer.Id, customer.Orders[0].CustomerId);

            var orders = GetCollection<Order>().Find(Query.EQ("CustomerId", customer.Id));
            Assert.IsNotNull(orders.Count() == 1);
        }
    }
}
