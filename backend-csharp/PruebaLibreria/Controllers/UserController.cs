using PruebaLibreria.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;
using System.Web.Mvc;
using System.Xml.Linq;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace PruebaLibreria.Controllers
{

    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private DTO_User DTO = new DTO_User();

        [HttpPost]
        [Route("crete-user")]
        public Models.Response postUser(Models.Request request)
        {
            return DTO.postUser(request);
        }
        [HttpPost]
        [Route("insert-bank")]
        public Models.Response postBank(Models.Request request)
        {
            return DTO.postBank(request);
        }
        [HttpPost]
        [Route("login")]
        public Models.Response login(Models.Request request)
        {
            return DTO.logIn(request);
        }
        [HttpPost]
        [Route("isAdmin")]
        public int isAdmin(Models.Request request)
        {
            return DTO.isAdmin(request);
        }
    }
}
