using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Weathery.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ApiController
    {
        public HomeController()
        {

        }

        //[Authorize]
        public IActionResult Index()
        {
            return Ok("Work");
        }
    }
}
