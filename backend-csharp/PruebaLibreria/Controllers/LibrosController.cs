using PruebaLibreria.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaLibreria.Controllers
{
    [RoutePrefix("api/libros")]
    public class LibrosController : ApiController
    {
        private DTO_Libro DTO = new DTO_Libro();
        [HttpGet]
        [Route("libros-controller")]
        public List<Models.Libro> getLibros()
        {
            return DTO.getLibros();
        }
        [HttpPut]
        [Route("libros-controller")]
        public Models.Response putLibros(Models.Request request)
        {
            return DTO.putLibro(request);
        }
        [HttpPost]
        [Route("libros-controller")]
        public Models.Response postLibros(Models.Request request)
        {
            return DTO.postLibro(request);
        }
        [HttpDelete]
        [Route("libros-controller")]
        public Models.Response deleteLibros(Models.Request request)
        {
            return DTO.deleteLibro(request);
        }
    }
}
