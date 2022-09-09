using ItChat.Services.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.ViewModels.Auth.ServerData
{
    public class AuthorizationSuccess
    {
        [JsonIgnore]
        private static bool isAlreadyFetchingAccessToken { get; set; } = false;

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public async Task SaveAccessToken()
        {
            await SecureStorage.Default.SetAsync("access_token", AccessToken);
        }

        public async Task SaveTokenType()
        {
            await SecureStorage.Default.SetAsync("token_type", TokenType);
        }

        public async Task SaveExpiresIn()
        {
            await SecureStorage.Default.SetAsync("expires_in", ExpiresIn);
        }

        public async Task SaveRefreshToken()
        {
            await SecureStorage.Default.SetAsync("refresh_token", RefreshToken);
        }

        public async Task SaveData()
        {
            await SaveAccessToken();
            await SaveTokenType();
            await SaveExpiresIn();
            await SaveRefreshToken();
        }

        public bool RemoveAccessToken()
        {
            return SecureStorage.Default.Remove("access_token");
        }

        public bool RemoveTokenType()
        {
            return SecureStorage.Default.Remove("token_type");
        }

        public bool RemoveExpiresIn()
        {
            return SecureStorage.Default.Remove("expires_in");
        }

        public bool RemoveRefreshToken()
        {
            return SecureStorage.Default.Remove("refresh_token");
        }

        public bool RemoveData()
        {
            return RemoveAccessToken() && RemoveTokenType() && RemoveExpiresIn() && RemoveRefreshToken();
        }

        public static async Task<bool> RefreshTokenAsync()
        {
            string refreshToken = await SecureStorage.Default.GetAsync("refresh_token");
            SecureStorage.Default.Remove("refresh_token");
            AuthorizationSuccess authorizationSuccess = null;

            if (! string.IsNullOrEmpty(refreshToken) && ! isAlreadyFetchingAccessToken)
            {
                isAlreadyFetchingAccessToken = true;
                authorizationSuccess = await Http.PostAsync<AuthorizationSuccess>("/auth/refresh_token", new { refresh_token = refreshToken });

                if (authorizationSuccess is AuthorizationSuccess)
                {
                    await authorizationSuccess.SaveData();

                    return true;
                }
            }

            return false;
        }
    }
}
