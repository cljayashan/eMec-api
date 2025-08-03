using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace emec.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Apply token authentication at the controller level  
    public class MathController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add([FromBody] int[] a)
        {
            int sum = a.Sum();
            return Ok(new { Result = sum });
        }
    }
}
