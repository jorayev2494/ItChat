using ItChat.Services.Http.HttpExceptions;
using ItChat.ViewModels.Auth.ServerData;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ItChat.Services.Http.Interceptors
{
    public class HttpInterceptors : DelegatingHandler
    {
        private HttpResponseMessage httpResponseMessage;

        public HttpInterceptors(): base(new HttpClientHandler())
        {

        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage httpRequestMessage,
            System.Threading.CancellationToken cancellationToken
        )
        {
            RepeatSendRequest:
            //System.Threading.Interlocked.Increment(ref _count);
            string accessToken = await SecureStorage.Default.GetAsync("access_token");
            //string refreshToken = await SecureStorage.Default.GetAsync("refresh_token");

            httpRequestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");
            httpRequestMessage.Headers.Add("Accept", "application/json");
            httpRequestMessage.Headers.Add("x-device-id", "dev-android-device");
            httpResponseMessage = await base.SendAsync(httpRequestMessage, cancellationToken);

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                bool isRefreshedToken = await AuthorizationSuccess.RefreshTokenAsync();

                if (isRefreshedToken)
                {
                    goto RepeatSendRequest;
                }
                else
                {
                    SecureStorage.Default.RemoveAll();
                    await Shell.Current.GoToAsync("/login");
                }
            }
            else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                string httpDataString = await httpResponseMessage.Content.ReadAsStringAsync();
                //ValidationException validationException = JsonConvert.DeserializeObject<ValidationException>(httpDataString);
                //MessagingCenter.Send<Shell, Dictionary<string, string[]>>(new Shell(), "validationException", validationException.Errors);

                //foreach (KeyValuePair<string, string[]> item in validationException.Errors)
                //{
                //    foreach (string msg in item.Value)
                //    {
                //        Console.WriteLine("--- server validation: '{0}' message: '{1}'", item.Key, msg);
                //    }
                //}

                Console.WriteLine(httpDataString);
            }

            return httpResponseMessage;

        }
    }
}
