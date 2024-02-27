using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace homeworkFeb5.Models
{
    public class NorthwindManger
    {
        private string _connectionString;
        public NorthwindManger(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Order> GetOrders()
        {
            return GetOrders(0);
        }

        public List<Order> GetOrders(int year)
        {
            string extraText = "WHERE YEAR(OrderDate) = @year";
            if (year == 0)
            {
                extraText = "";
            }
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT O.OrderID, OrderDate, UnitPrice, Quantity  FROM Orders O\r\nJOIN [Order Details] OD\r\nON O.OrderID = OD.OrderID\r\n{extraText}";
            command.Parameters.AddWithValue("@year", year);
            connection.Open();
            List<Order> orders = new List<Order>();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["OrderId"];
                bool check = orders.Any(o => o.OrderID == id);
                if (!check)
                {
                    orders.Add(new Order
                    {
                        OrderDate = (DateTime)reader["OrderDate"],
                        OrderID = (int)reader["OrderId"]
                    });
                }

                orders.First(o => o.OrderID == id).orderDetails.Add(new OrderDetails
                {
                    OrderID = id,
                    Quantity = (short)reader["Quantity"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                });
            }
            connection.Close();
            return orders;
        }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetails> orderDetails { get; set; } = new List<OrderDetails>();
    }

    public class OrderDetails
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
    }
}