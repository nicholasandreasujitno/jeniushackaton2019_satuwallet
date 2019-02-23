using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace satuwallet_android.Models
{
    public class AccessTokenBinding
    {
        public string client_id { get; set; }
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class RefreshTokenBinding
    {
        public string client_id { get; set; }
        public string grant_type { get; set; }
        public string refresh_token { get; set; }
    }

    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }

        public string userName { get; set; }

        [JsonProperty("as:client_id")]
        public string clientId { get; set; }
        [JsonProperty(".issued")]
        public DateTime IssuedDateUtc { get; set; }
        [JsonProperty(".expires")]
        public DateTime ExpiredDateUtc { get; set; }

        public bool IsExpired()
        {
            if (DateTime.UtcNow < ExpiredDateUtc)
            {
                return false;
            }
            return true;
        }
    }
}