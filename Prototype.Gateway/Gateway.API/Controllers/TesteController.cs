using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TesteController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("dawa");
    }
}
