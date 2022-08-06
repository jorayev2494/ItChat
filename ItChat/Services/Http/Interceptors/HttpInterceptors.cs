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

            httpRequestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");
            httpRequestMessage.Headers.Add("Accept", "application/json");

            try
            {
                httpResponseMessage = await base.SendAsync(httpRequestMessage, cancellationToken);

                return httpResponseMessage;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("--- StatusCode: '{0}' Message: '{1}' Data: '{2}'", ex.StatusCode, ex.Message, ex.Data);

                string httpDataString = await httpResponseMessage.Content.ReadAsStringAsync();

                switch (ex.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        //UnauthorizedException unauthorizedException = JsonConvert.DeserializeObject<UnauthorizedException>(httpDataString);

                        if (await AuthorizationSuccess.RefreshTokenAsync())
                        {
                            goto RepeatSendRequest; 
                        }
                        else
                        {
                            SecureStorage.Default.RemoveAll();
                            await Shell.Current.GoToAsync("/login");
                        }


                        break;

                    case System.Net.HttpStatusCode.BadRequest:
                        Console.WriteLine("");
                        break;

                    case System.Net.HttpStatusCode.Forbidden:
                        Console.WriteLine("");
                        break;

                    case System.Net.HttpStatusCode.UnprocessableEntity:
                        ValidationException validationException = JsonConvert.DeserializeObject<ValidationException>(httpDataString);
                        MessagingCenter.Send<Shell, Dictionary<string, string[]>>(new Shell(), "validationException", validationException.Errors);

                        foreach (KeyValuePair<string, string[]> item in validationException.Errors)
                        {
                            foreach (string msg in item.Value)
                            {
                                Console.WriteLine("--- server validation: '{0}' message: '{1}'", item.Key, msg);
                            }
                        }

                        break;

                    default:
                        break;
                }

                // httpResponseMessage.EnsureSuccessStatusCode();

                throw new HttpRequestException(ex.Message);
            }

        }
    }
}
