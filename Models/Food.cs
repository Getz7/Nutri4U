using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Food
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "marca")]
        public string marca { get; set; } = "";

        [JsonProperty(PropertyName = "nombreCategoria")]
        public string nombreCategoria { get; set; } = "";

        [JsonProperty(PropertyName = "nombre")]
        public string nombre { get; set; } = "";

        [JsonProperty(PropertyName = "porcionGramos")]
        public string  porcionGramos { get; set; } = "";

        [JsonProperty(PropertyName = "porcionCasera")]
        public string porcionCasera { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioHarina")]
        public string  intercambioHarina { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioLacteoDescremado")]
        public string  intercambioLacteoDescremado { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioLacteoSemi")]
        public string  intercambioLacteoSemi { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioLacteoEntero")]
        public string  intercambioLacteoEntero { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioGrasa")]
        public string  intercambioGrasa { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioCarneMagra")]
        public string  intercambioCarneMagra { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioCarneSemi")]
        public string  intercambioCarneSemi { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioCarneGrasa")]
        public string  intercambioCarneGrasa { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioLeguminosa")]
        public string  intercambioLeguminosa { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioFruta")]
        public string  intercambioFruta { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioAzucar")]
        public string intercambioAzucar { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioVegetal")]
        public string  intercambioVegetal { get; set; } = "";

        [JsonProperty(PropertyName = "intercambioLibre")]
        public bool intercambioLibre { get; set; } = false;

        [JsonProperty(PropertyName = "photo")]
        public string photo { get; set; } = "";

    }
}
