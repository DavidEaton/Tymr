using Microsoft.AspNetCore.Mvc;

namespace Tymr.Api.Features
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
            // Constructor for the controller.
            // In a more complex application, dependencies such as services would be injected here.
        }

        [HttpGet]
        public IActionResult GetTestValue()
        {
            // Return a simple integer value with a 200 OK status; 42 is chosen as a simple example value.
            return Ok(42);
        }
    }
}
