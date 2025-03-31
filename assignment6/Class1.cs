using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem
{
    public class OrderItem
    {
        public string Product { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float Total => Amount * Price;

        public override bool Equals(object obj) => obj is OrderItem item && Product == item.Product;
        public override int GetHashCode() => Product.GetHashCode();
        public override string ToString() => $"{Product} ×{Amount} ¥{Price} (小计:¥{Total})";
    }

    public class Order
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public List<OrderItem> Items { get; } = new List<OrderItem>();
        public float Total => Items.Sum(i => i.Total);

        public override bool Equals(object obj) => obj is Order order && ID == order.ID;
        public override int GetHashCode() => ID.GetHashCode();
        public override string ToString() =>
            $"订单号:{ID} 客户:{Customer} 总金额:¥{Total}\n明细:\n{string.Join("\n", Items)}\n";
    }

    public class OrderManager
    {
        private List<Order> data = new List<Order>();

        public void Add(Order newOrder)
        {
            if (data.Any(o => o.ID == newOrder.ID))
                throw new Exception("订单号重复！");
            data.Add(newOrder);
        }

        public void Delete(int id)
        {
            var order = data.FirstOrDefault(o => o.ID == id);
            if (order == null) throw new Exception("找不到订单");
            data.Remove(order);
        }

        public void Update(Order newOrder)
        {
            var index = data.FindIndex(o => o.ID == newOrder.ID);
            if (index == -1) throw new Exception("订单不存在");
            data[index] = newOrder;
        }

        // 通用查询，这里可以传lambda，unittest可以测测看
        public List<Order> Find(Func<Order, bool> condition)
            => data.Where(condition).OrderBy(o => o.Total).ToList();

        public void Sort(Func<Order, Order, int> compare = null)
        {
            data.Sort(compare == null ? (a, b) => a.ID.CompareTo(b.ID) :
                new Comparison<Order>(compare));
        }


    }
    class OrderOperate
    {
        

        public static void Add(string id, string customer, string name, string num, string money, OrderManager manager)
        {
            try
            {
                var o = new Order();
                o.ID = int.Parse(id);
                o.Customer = customer;

                var item = new OrderItem
                {
                    Product = name,
                    Amount = int.Parse(num),
                    Price = float.Parse(money)
                };

                if (o.Items.Contains(item)) MessageBox.Show("商品重复！");
                else o.Items.Add(item);

                manager.Add(o);
                MessageBox.Show("添加成功！");
            }
            catch (Exception e) { MessageBox.Show($"出错：{e.Message}"); }
        }

        public static void Remove(string id, OrderManager manager)
        {
            try
            {
                manager.Delete(int.Parse(id));
                MessageBox.Show("删除成功！");
            }
            catch (Exception e) { MessageBox.Show($"出错：{e.Message}"); }
        }

        public static void Modify(string id_, string customer, string name, string num, string money, OrderManager manager)
        {
            try
            {   int id;
                int.TryParse(id_, out id);
                var old = manager.Find(o => o.ID == id).FirstOrDefault() ?? throw new Exception("订单不存在");

                var newOrder = new Order { ID = id, Customer = old.Customer };
                old.Items.ForEach(i => newOrder.Items.Add(i));

                newOrder.Customer = string.IsNullOrEmpty(customer) ? old.Customer : customer;                             
                var item = new OrderItem
                {
                    Product = name,
                    Amount = int.Parse(num),
                    Price = float.Parse(money)
                };
                if (newOrder.Items.Contains(item)) MessageBox.Show("商品已存在！");
                else newOrder.Items.Add(item);
                manager.Update(newOrder);
                MessageBox.Show("修改成功！");
            }
            catch (Exception e) { MessageBox.Show($"出错：{e.Message}"); }
        }

        static void Search(OrderManager manager)
        {
            try
            {
                MessageBox.Show("1.所有订单 2.按订单号 3.按商品 4.按客户 5.按金额区间");
                Console.Write("请选择:");
                var choice = Console.ReadLine();
                List<Order> result = null;

                switch (choice)
                {
                    case "1":
                        result = manager.Find(o => true);
                        break;
                    case "2":
                        Console.Write("输入订单号:");
                        result = manager.Find(o => o.ID == int.Parse(Console.ReadLine()));
                        break;
                    case "3":
                        Console.Write("输入商品名:");
                        result = manager.Find(o => o.Items.Any(i => i.Product == Console.ReadLine()));
                        break;
                    case "4":
                        Console.Write("输入客户名:");
                        result = manager.Find(o => o.Customer == Console.ReadLine());
                        break;
                    case "5":
                        Console.Write("最低金额:");
                        float min = float.Parse(Console.ReadLine());
                        Console.Write("最高金额:");
                        result = manager.Find(o => o.Total >= min && o.Total <= float.Parse(Console.ReadLine()));
                        break;
                }

                if (result?.Count > 0) result.ForEach(Console.WriteLine);
                else MessageBox.Show("没有找到订单");
            }
            catch (Exception e) { MessageBox.Show($"出错：{e.Message}"); }
        }
    }
}
