using Microsoft.AspNetCore.Mvc;

namespace Weathery.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
