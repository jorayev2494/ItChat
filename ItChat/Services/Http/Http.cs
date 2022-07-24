using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Services.Http
{
    public class Http
    {

        private const string SERVER_URL = "http://192.168.1.107:8000/api";

        private static async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(SERVER_URL),
            };

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            string accessToken = await SecureStorage.Default.GetAsync("access_token");

            if (accessToken != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            }

            return client;
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            HttpClient httpClient = await GetClient();
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"http://192.168.1.107:8000/api{url}");
            string stringContent = await httpResponse.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(stringContent);

            return result;
        }

        public static async Task<T> PostAsync<T>(string url, object data)
        {
            HttpClient httpClient = await GetClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await httpClient.PostAsync($"http://192.168.1.107:8000/api{url}", httpContent);
            string stringContent = await httpResponse.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(stringContent);

            return result;
        }

        public static async Task<T> PostAsync<T>(string url, MultipartFormDataContent multipartFormData)
        {
            HttpClient httpClient = await GetClient();
            HttpResponseMessage httpResponse = await httpClient.PostAsync($"http://192.168.1.107:8000/api{url}", multipartFormData);
            string stringContent = await httpResponse.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(stringContent);

            return result;
        }

        public static async Task<T> PutAsync<T>(string url, object data)
        {
            HttpClient httpClient = await GetClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await httpClient.PutAsync($"http://192.168.1.107:8000/api{url}", httpContent);
            string stringContent = await httpResponse.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(stringContent);

            return result;
        }

    }
}
