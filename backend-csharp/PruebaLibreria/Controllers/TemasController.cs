using PruebaLibreria.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaLibreria.Controllers
{
    [RoutePrefix("api/temas")]
    public class TemasController : ApiController
    {
        private DTO_Tema DTO = new DTO_Tema();
        [HttpGet]
        [Route("temas-controller")]
        public List<Models.Tema> getTemas()
        {
            return DTO.getTemas();
        }
        [HttpPut]
        [Route("temas-controller")]
        public Models.Response putTemas(Models.Request request)
        {
            return DTO.putTema(request);
        }
        [HttpPost]
        [Route("temas-controller")]
        public Models.Response postTemas(Models.Request request)
        {
            return DTO.postTema(request);
        }
        [HttpDelete]
        [Route("temas-controller")]
        public Models.Response deleteTemas(Models.Request request)
        {
            return DTO.deleteTema(request);
        }
    }
}
