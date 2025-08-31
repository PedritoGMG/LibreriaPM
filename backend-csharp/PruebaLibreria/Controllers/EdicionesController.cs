using PruebaLibreria.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaLibreria.Controllers
{
    [RoutePrefix("api/ediciones")]
    public class EdicionesController : ApiController
    {
        private DTO_Edicion DTO = new DTO_Edicion();
        [HttpGet]
        [Route("ediciones-controller")]
        public List<Models.Edicion> getEdiciones()
        {
            return DTO.getEdiciones();
        }
        [HttpPut]
        [Route("ediciones-controller")]
        public Models.Response putEdiciones(Models.Request request)
        {
            return DTO.putEdicion(request);
        }
        [HttpPost]
        [Route("ediciones-controller")]
        public Models.Response postEdiciones(Models.Request request)
        {
            return DTO.postEdicion(request);
        }
        [HttpDelete]
        [Route("ediciones-controller")]
        public Models.Response deleteEdiciones(Models.Request request)
        {
            return DTO.deleteEdicion(request);
        }
    }
}
