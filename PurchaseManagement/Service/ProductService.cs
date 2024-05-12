using Bussiness.Cart;
using Bussiness.Product;
using Bussiness.User;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using PurchaseManagement.IService;
using System.Net.Http.Headers;
using System.Text;

namespace PurchaseManagement.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("CustomHttpClient");
        }

        public async Task<List<Products>> GetAll()
        {
            List<Products> prod = new List<Products>();
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync("api/Products");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    prod = JsonConvert.DeserializeObject<List<Products>>(result);
                }
                else
                {
                    // Log the unsuccessful HTTP response
                    Console.WriteLine($"HTTP request failed with status code: {httpResponse.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the HTTP request exception
                Console.WriteLine($"HTTP request failed: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log the JSON deserialization exception
                Console.WriteLine($"JSON deserialization failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return prod;
        }

        public async Task<Products> Add(Products products)
        {
            Products prod = new Products();
            try
            {
                var jsonRequestData = JsonConvert.SerializeObject(products);
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.PostAsync("api/addproduct", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    prod = JsonConvert.DeserializeObject<Products>(result);
                }
                else
                {
                    // Log the unsuccessful HTTP response
                    Console.WriteLine($"HTTP request failed with status code: {httpResponse.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the HTTP request exception
                Console.WriteLine($"HTTP request failed: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log the JSON deserialization exception
                Console.WriteLine($"JSON deserialization failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return prod;
        }

        public async Task<Products> Delete(int productId)
        {
            Products products = new Products();
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.DeleteAsync($"api/products/{productId}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<Products>(result);
                }
                else
                {
                    // Log the unsuccessful HTTP response
                    Console.WriteLine($"HTTP request failed with status code: {httpResponse.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the HTTP request exception
                Console.WriteLine($"HTTP request failed: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log the JSON deserialization exception
                Console.WriteLine($"JSON deserialization failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return products;
        }

        public async Task<Products> Update(Products products)
        {
            Products prod = new Products();
            try
            {
                var jsonRequestData = JsonConvert.SerializeObject(products);
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.PutAsync("api/updateproducts", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    prod = JsonConvert.DeserializeObject<Products>(result);
                }
                else
                {
                    // Log the unsuccessful HTTP response
                    Console.WriteLine($"HTTP request failed with status code: {httpResponse.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the HTTP request exception
                Console.WriteLine($"HTTP request failed: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log the JSON deserialization exception
                Console.WriteLine($"JSON deserialization failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return prod;
        }
    }
}

