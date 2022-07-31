using PusherClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItChat.Services.Pusher
{
    public class CustomPusherAuthorizer : HttpAuthorizer
    {
        public CustomPusherAuthorizer(string authEndpoint) : base(authEndpoint)
        {

            //

        }

        public override void PreAuthorize(HttpClient httpClient)
        {
            base.PreAuthorize(httpClient);

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("X-App-ID", "laravel_rdb");
        }
    }
}
