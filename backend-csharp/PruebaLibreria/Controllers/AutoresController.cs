using PruebaLibreria.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaLibreria.Controllers
{
    [RoutePrefix("api/autores")]
    public class AutoresController : ApiController
    {
        private DTO_Autor DTO = new DTO_Autor();
        [HttpGet]
        [Route("autores-controller")]
        public List<Models.Autor> getAutores()
        {
            return DTO.getAutores();
        }
        [HttpPut]
        [Route("autores-controller")]
        public Models.Response putAutores(Models.Request request)
        {
            return DTO.putAutor(request);
        }
        [HttpPost]
        [Route("autores-controller")]
        public Models.Response postAutores(Models.Request request)
        {
            return DTO.postAutor(request);
        }
        [HttpDelete]
        [Route("autores-controller")]
        public Models.Response deleteAutores(Models.Request request)
        {
            return DTO.deleteAutor(request);
        }
    }
}
