using System;

namespace assignment9.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(int id, string customer, double amount, DateTime orderDate)
        {
            if (string.IsNullOrWhiteSpace(customer))
                throw new ArgumentException("Customer name cannot be empty.");
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            (Id, Customer, Amount, OrderDate) = (id, customer, amount, orderDate);
        }

        public override string ToString() =>
            $"Order [Id={Id}, Customer={Customer}, Amount={Amount}, OrderDate={OrderDate}]";
    }
}