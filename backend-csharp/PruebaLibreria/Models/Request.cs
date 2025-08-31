using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellidos { get; set; }
        public string CP { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string PW { get; set; }


        public string NumberCard { get; set; }
        public string CVV { get; set; }
        public string NombreTitular { get; set; }

        public string Tema { get; set; }
        public string Edicion {  get; set; }
        public string Formato { get; set; }
        public string Autor {  get; set; }
        public float Precio { get; set; }

        public string Isbn { get; set; }
        public int Cantidad { get; set; }
        public string ImgName { get; set; }
    }
}