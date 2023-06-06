using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class GeneController : Controller
{
    [HttpGet("[action]")]
    public ActionResult GetAllGenes()
    {
        return Ok();
    }

    [HttpGet("[action]/{id}")]
    public ActionResult GetGeneByGuid(Guid id)
    {
        return Ok();
    }
}