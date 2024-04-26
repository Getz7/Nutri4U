using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class UserFood
    {

        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "idUsuario")]
        public string? idUsuario { get; set; }

        [JsonProperty(PropertyName = "idAlimento")]
        public string idAlimento { get; set; } = "";
    }
}