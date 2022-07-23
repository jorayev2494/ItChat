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
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

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

        public async Task SaveData()
        {
            await SaveAccessToken();
            await SaveTokenType();
            await SaveExpiresIn();
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

        public bool RemoveData()
        {
            return RemoveAccessToken() && RemoveTokenType() && RemoveExpiresIn();
        }
    }
}
