using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Category
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; } = "";
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; } = "";

    }
}
