using ItChat.Services.Http.Interceptors;
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

        private const string SERVER_URL = "http://185.81.167.88:8000/api";

        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient(new HttpInterceptors());

            client.BaseAddress = new Uri(SERVER_URL);

            return client;
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            HttpClient httpClient = GetClient();
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"http://185.81.167.88:8000/api{url}");
            httpResponse.EnsureSuccessStatusCode();
            string stringContent = await httpResponse.Content.ReadAsStringAsync();

            T result = default(T);
            if (httpResponse.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(stringContent);
            }

            return result;
        }

        public static async Task<T> PostAsync<T>(string url, object data)
        {
            HttpClient httpClient = GetClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await httpClient.PostAsync($"http://185.81.167.88:8000/api{url}", httpContent);
            string stringContent = await httpResponse.Content.ReadAsStringAsync();

            T result = default(T);
            if (httpResponse.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(stringContent);
            }

            return result;
        }

        public static async Task<T> PostAsync<T>(string url, MultipartFormDataContent multipartFormData)
        {
            HttpClient httpClient = GetClient();
            HttpResponseMessage httpResponse = await httpClient.PostAsync($"http://185.81.167.88:8000/api{url}", multipartFormData);
            string stringContent = await httpResponse.Content.ReadAsStringAsync();

            T result = default(T);
            if (httpResponse.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(stringContent);
            }

            return result;
        }

        public static async Task<T> PutAsync<T>(string url, object data)
        {
            HttpClient httpClient = GetClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await httpClient.PutAsync($"http://185.81.167.88:8000/api{url}", httpContent);
            string stringContent = await httpResponse.Content.ReadAsStringAsync();

            T result = default(T);
            if (httpResponse.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(stringContent);
            }

            return result;
        }

    }
}
