using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDepartamentos.Models
{
    public class Departamento
    {
        [JsonProperty("idDepartamento")]
        public int IdDepartamento { get; set; }
        [JsonProperty("nombre")]
        public String Nombre { get; set; }
        [JsonProperty("localidad")]
        public String Localidad { get; set; }
    }
}
