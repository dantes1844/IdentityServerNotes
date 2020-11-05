using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [Route("identity")]
    [ApiController]
    //[Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Debug.WriteLine("执行到API里了");

            //return new JsonResult(new JObject(){"age",12});
            ////return Content("ok00");

            var list = from c in User.Claims
                select new { c.Type, c.Value };
            return new JsonResult(list);
        }
    }
}
