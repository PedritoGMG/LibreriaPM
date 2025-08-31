using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Models
{
    public class Libro
    {
        public int id {  get; set; }
        public string nombre { get; set; }
        public string autor {  get; set; }
        public string edicion {  get; set; }
        public string formato { get; set; }
        public string tema {  get; set; }
        public float precio {  get; set; }
        public string isbn { get; set; }
        public int cantidad { get; set; }
        public string imgname { get; set; }


    }
}