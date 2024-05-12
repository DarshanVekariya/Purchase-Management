using Bussiness.Cart;
using Bussiness.Product;
using Bussiness.Purchaseorder;
using Bussiness.User;
using Newtonsoft.Json;
using PurchaseManagement.IService;
using System.Text;

namespace PurchaseManagement.Service
{
    public class CartService: ICartService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("CustomHttpClient");
        }

        public async Task<List<Products>> Add(int productId,int userId)
        {
            List<Products> cartIteams = new List<Products>();
            try
            {
                CartRequestModel cartrequest = new CartRequestModel { ProductId = productId, UserId = userId};
                var jsonRequestData = JsonConvert.SerializeObject(cartrequest);
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.PostAsync("api/addtocart", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    cartIteams = JsonConvert.DeserializeObject<List<Products>>(result);
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
            return cartIteams;
        }

        public async Task<ChkExistInCartModel> ChkExist(int productId, int userId)
        {
            ChkExistInCartModel chkExistInCartModel = new ChkExistInCartModel();    
            try
            {
                CartRequestModel cartrequest = new CartRequestModel { ProductId = productId, UserId = userId };
                var jsonRequestData = JsonConvert.SerializeObject(cartrequest);
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.PostAsync("api/chkexist", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    chkExistInCartModel = JsonConvert.DeserializeObject<ChkExistInCartModel>(result);
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
            return chkExistInCartModel;
        }

        public async Task<List<Products>> Remove(int productId, int userId)
        {
            List<Products> cartIteams = new List<Products>();
            try
            {
                CartRequestModel cartrequest = new CartRequestModel { ProductId = productId, UserId = userId };
                var jsonRequestData = JsonConvert.SerializeObject(cartrequest);
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"api/remove", UriKind.Relative),
                    Content = content
                });

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    cartIteams = JsonConvert.DeserializeObject<List<Products>>(result);
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
            return cartIteams;
        }

        public async Task<List<PurchaseOrder>> PurchaseOrder(List<PurchaseOrder> purchaseOrder)
        {
            List<PurchaseOrder> purchaseOrderIteams = new List<PurchaseOrder>();
            try
            {
                var jsonRequestData = JsonConvert.SerializeObject(purchaseOrder);
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await _httpClient.PostAsync("api/purchaseorder", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    purchaseOrderIteams = JsonConvert.DeserializeObject<List<PurchaseOrder>>(result);
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
            return purchaseOrderIteams;
        }

        public async Task<List<PurchaseOrder>> PurchaseOrderList(int id)
        {
            List<PurchaseOrder> purchaseOrderIteams = new List<PurchaseOrder>();
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync($"api/purchaseorder/{id}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    purchaseOrderIteams = JsonConvert.DeserializeObject<List<PurchaseOrder>>(result);
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
            return purchaseOrderIteams;
        }
    }
}
