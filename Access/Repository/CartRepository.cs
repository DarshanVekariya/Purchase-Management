using Access.Interface;
using Bussiness.Cart;
using Bussiness.db;
using Bussiness.Product;
using Bussiness.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Access.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration _configuration;

        public CartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Products>> Add(int productId,int userId)
        {
            List<Products> cartItems = new List<Products>();
            string connectionString = _configuration.GetConnectionString("PurchaseManagementDB");
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("AddToCart", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader reader = await command.ExecuteReaderAsync();


                        // Retrieve cart items after adding the product

                        while (reader.Read())
                        {
                            cartItems.Add(new Products
                            {
                                Id = (int)reader["Id"],
                                Name = Convert.ToString(reader["Name"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            });
                        }

                        reader.Close();
                    }
                }

                return cartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return cartItems;
            }
        }

        public async Task<ChkExistInCartModel> ChkExist(int productId, int userId)
        {
            ChkExistInCartModel chkExistInCartModel = new ChkExistInCartModel();
            string connectionString = _configuration.GetConnectionString("PurchaseManagementDB");
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("ChkExistInCart", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader reader = await command.ExecuteReaderAsync();


                        // Retrieve cart items after adding the product

                        if (reader.Read())
                        {
                            chkExistInCartModel.message = Convert.ToString(reader["message"]);
                            chkExistInCartModel.isexist = Convert.ToBoolean(reader["isexist"]);
                        }

                        reader.Close();
                    }
                }

                return chkExistInCartModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return chkExistInCartModel;
            }
        }

        public async Task<List<Products>> Remove(int productId, int userId)
        {
            List<Products> cartItems = new List<Products>();
            string connectionString = _configuration.GetConnectionString("PurchaseManagementDB");
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("RemoveToCart", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader reader = await command.ExecuteReaderAsync();


                        // Retrieve cart items after adding the product

                        while (reader.Read())
                        {
                            cartItems.Add(new Products
                            {
                                Id = (int)reader["Id"],
                                Name = Convert.ToString(reader["Name"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            });
                        }

                        reader.Close();
                    }
                }

                return cartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return cartItems;
            }
        }


    }
}
