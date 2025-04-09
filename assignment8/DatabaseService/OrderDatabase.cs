using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OrderApp
{
    public class MyDatabase
    {
        private string connectionString;

        public MyDatabase()
        {
            connectionString = "Server = 127.0.0.1; port=3306; Database=test1; User=root; Password=123456; CharSet=utf8; ";
        }
        public MyDatabase(string server, string database, string user, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};";
        }

        // 添加订单
        public void AddOrder(Order order)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {

                    string orderSql = "INSERT INTO Orders (OrderId, CustomerName, CreateTime) VALUES (@OrderId, @CustomerName, @CreateTime)";
                    MySqlCommand cmd = new MySqlCommand(orderSql, conn, transaction);
                    cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                    cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                    cmd.Parameters.AddWithValue("@CreateTime", order.CreateTime);
                    cmd.ExecuteNonQuery();

                    string detailSql = "INSERT INTO OrderDetails (OrderId, ProductName, Quantity, UnitPrice) VALUES (@OrderId, @ProductName, @Quantity, @UnitPrice)";
                    foreach (var detail in order.Details)
                    {
                        cmd.CommandText = detailSql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                        cmd.Parameters.AddWithValue("@ProductName", detail.ProductName);
                        cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                        cmd.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException("添加订单失败", ex);
                }
            }
        }

  
        public void DeleteOrder(int orderId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 删除OrderDetails
                    string deleteDetailsSql = "DELETE FROM OrderDetails WHERE OrderId = @OrderId";
                    MySqlCommand cmd = new MySqlCommand(deleteDetailsSql, conn, transaction);
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.ExecuteNonQuery();

                    // 删除Order
                    string deleteOrderSql = "DELETE FROM Orders WHERE OrderId = @OrderId";
                    cmd.CommandText = deleteOrderSql;
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException("删除订单失败", ex);
                }
            }
        }

        // 更新订单
        public void UpdateOrder(Order newOrder)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string updateOrderSql = "UPDATE Orders SET CustomerName = @CustomerName, CreateTime = @CreateTime WHERE OrderId = @OrderId";
                    MySqlCommand cmd = new MySqlCommand(updateOrderSql, conn, transaction);
                    cmd.Parameters.AddWithValue("@CustomerName", newOrder.CustomerName);
                    cmd.Parameters.AddWithValue("@CreateTime", newOrder.CreateTime);
                    cmd.Parameters.AddWithValue("@OrderId", newOrder.OrderId);
                    int affected = cmd.ExecuteNonQuery();
                    if (affected == 0)
                        throw new ApplicationException($"订单{newOrder.OrderId}不存在，无法更新。");


                    string deleteDetailsSql = "DELETE FROM OrderDetails WHERE OrderId = @OrderId";
                    cmd.CommandText = deleteDetailsSql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@OrderId", newOrder.OrderId);
                    cmd.ExecuteNonQuery();


                    string insertDetailSql = "INSERT INTO OrderDetails (OrderId, ProductName, Quantity, UnitPrice) VALUES (@OrderId, @ProductName, @Quantity, @UnitPrice)";
                    foreach (var detail in newOrder.Details)
                    {
                        cmd.CommandText = insertDetailSql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@OrderId", newOrder.OrderId);
                        cmd.Parameters.AddWithValue("@ProductName", detail.ProductName);
                        cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                        cmd.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException("更新订单失败", ex);
                }
            }
        }

        // 获取单个订单
        public Order GetOrder(int orderId)
        {
            Order order = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // 查询Orders表
                string orderSql = "SELECT OrderId, CustomerName, CreateTime FROM Orders WHERE OrderId = @OrderId";
                MySqlCommand cmd = new MySqlCommand(orderSql, conn);
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // 使用构造函数初始化Order对象
                        order = new Order(
                            orderId: reader.GetInt32("OrderId"),
                            customer: new Customer { Name = reader.GetString("CustomerName") },
                            items: new List<OrderDetail>() // 初始化空的Details列表
                        );
                        order.CreateTime = reader.GetDateTime("CreateTime");
                    }
                    else
                    {
                        return null;
                    }
                }

                // 查询OrderDetails表
                string detailSql = "SELECT ProductName, Quantity, UnitPrice FROM OrderDetails WHERE OrderId = @OrderId";
                cmd.CommandText = detailSql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                using (var detailReader = cmd.ExecuteReader())
                {
                    while (detailReader.Read())
                    {

                    }
                }
            }
            return order;
        }

        // 获取所有订单（优化查询）
        public List<Order> GetAllOrders()
        {
            var orders = new Dictionary<int, Order>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT o.OrderId, o.CustomerName, o.CreateTime, 
                           od.ProductName, od.Quantity, od.UnitPrice 
                    FROM Orders o 
                    LEFT JOIN OrderDetails od ON o.OrderId = od.OrderId";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int orderId = reader.GetInt32("OrderId");
                        if (!orders.TryGetValue(orderId, out Order order))
                        {
                            // 使用构造函数初始化Order对象
                            order = new Order(
                                orderId: orderId,
                                customer: new Customer { Name = reader.GetString("CustomerName") },
                                items: new List<OrderDetail>() // 初始化空的Details列表
                            );
                            order.CreateTime = reader.GetDateTime("CreateTime");
                            orders.Add(orderId, order);
                        }
                    }
                }
            }
            return new List<Order>(orders.Values);
        }
    }
}