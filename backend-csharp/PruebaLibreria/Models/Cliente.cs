using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Models
{
    public class Cliente
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string cp { get; set; }
        public string direccion { get; set; }
        public string poblacion { get; set; }
        public string dni { get; set; }
        public string email { get; set; }
    }
}