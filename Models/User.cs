using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Modelo
{
    public class User
    {
        [JsonProperty(PropertyName = "cedula")]
        public string? idCard { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; } = "";

        [JsonProperty(PropertyName = "lastname")]
        public string lastname { get; set; } = "";

        [JsonProperty(PropertyName = "age")]
        public int age { get; set; } = 0;
        [JsonProperty(PropertyName = "role")]
        public string role { get; set; } = "";
        [JsonProperty(PropertyName = "email")]
        public string email { get; set; } = "";
        [JsonProperty(PropertyName = "password")]
        public string password { get; set; } = "";
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; } = "";
    }
}
