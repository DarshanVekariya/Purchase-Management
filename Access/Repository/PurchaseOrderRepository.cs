using Access.Interface;
using Bussiness.Purchaseorder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace Access.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly IConfiguration _configuration;

        public PurchaseOrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<PurchaseOrder>> GenerateOrderAndItems(List<PurchaseOrder> orderItems)
        {
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            string connectionString = _configuration.GetConnectionString("PurchaseManagementDB");
            try
            {
                // Convert list of items to DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductId", typeof(int));
                dt.Columns.Add("Quantity", typeof(int));
                foreach (var item in orderItems)
                {
                    dt.Rows.Add(item.ProductId, item.Quantity);
                }

                // Pass DataTable as a parameter to the stored procedure
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("GenerateOrderAndItems", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", orderItems[0].UserId);
                        SqlParameter parameter = command.Parameters.AddWithValue("@OrderItems", dt);
                        parameter.SqlDbType = SqlDbType.Structured;

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        // Retrieve purchase order items from the stored procedure result
                        while (reader.Read())
                        {
                            purchaseOrderList.Add(new PurchaseOrder
                            {
                                ProductId = Convert.ToInt32(reader["Id"]),
                                ProductName = Convert.ToString(reader["Name"]),
                                ProductPrice = Convert.ToDecimal(reader["Price"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            });
                        }

                        reader.Close();
                    }
                }

                return purchaseOrderList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return purchaseOrderList;
            }
        }


        public async Task<List<PurchaseOrder>> GetList(int UserId)
        {
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            string connectionString = _configuration.GetConnectionString("PurchaseManagementDB");
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("GenerateOrderList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", UserId);

                        SqlDataReader reader = await command.ExecuteReaderAsync();


                        // Retrieve cart items after adding the product

                        while (reader.Read())
                        {
                            purchaseOrderList.Add(new PurchaseOrder
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = Convert.ToString(reader["ProductName"]),
                                ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                OrderDate = DateTime.ParseExact(Convert.ToString(reader["OrderDate"]), "M/d/yyyy h:mm:ss tt",
                                CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm")
                                
                            });
                        }

                        reader.Close();
                    }
                }

                return purchaseOrderList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return purchaseOrderList;
            }
        }

    }
}
