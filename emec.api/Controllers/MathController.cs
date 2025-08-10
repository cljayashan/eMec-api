using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace emec.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]   
    public class MathController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add([FromBody] int[] a)
        {
            int sum = a.Sum();
            return Ok(new { Result = sum });
        }

        [HttpPost("User")]
        public IActionResult User([FromBody] string name)
        {
            return Ok("Hello User : " + name);
        }

        [HttpPost("Admin")]
        public IActionResult Admin([FromBody] string name)
        {
            return Ok("Hello Admin : " + name);
        }

    }


}
