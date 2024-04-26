using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Plans
    {
        [JsonProperty(PropertyName = "id")]
        public string? idPdf { get; set; }

        [JsonProperty(PropertyName = "idUsuario")]
        public User? idUser { get; set; }
        [JsonProperty(PropertyName = "pdfContent")]
        public string pdfContent { get; set; } = "";
    }
}
