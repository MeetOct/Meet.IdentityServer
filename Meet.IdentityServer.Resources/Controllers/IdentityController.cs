using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Meet.IdentityServer.Resources.Controllers
{
    [Route("identity")]
    public class IdentityController : Controller
    {

        [HttpGet]
        [Authorize(Policy = "Policy")]
        public IActionResult Get()
        {
            return new JsonResult(from a in User.Claims select new { a.Type, a.Value });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Policy1")]
        public IActionResult Get(int id)
        {
            return new JsonResult(from a in User.Claims select new { a.Type, a.Value });
        }
    }
}
