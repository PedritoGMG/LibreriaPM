using PruebaLibreria.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaLibreria.Controllers
{
    [RoutePrefix("api/formatos")]
    public class FormatosController : ApiController
    {
        private DTO_Formato DTO = new DTO_Formato();
        [HttpGet]
        [Route("formatos-controller")]
        public List<Models.Formato> getFormatos()
        {
            return DTO.getFormatos();
        }
        [HttpPut]
        [Route("formatos-controller")]
        public Models.Response putFormatos(Models.Request request)
        {
            return DTO.putFormato(request);
        }
        [HttpPost]
        [Route("formatos-controller")]
        public Models.Response postFormatos(Models.Request request)
        {
            return DTO.postFormato(request);
        }
        [HttpDelete]
        [Route("formatos-controller")]
        public Models.Response deleteFormatos(Models.Request request)
        {
            return DTO.deleteFormato(request);
        }
    }
}
