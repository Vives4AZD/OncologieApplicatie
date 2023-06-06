using Microsoft.AspNetCore.Mvc;
using OncologieApplicatie.Core.Controllers.OncologieApplicatie.Services;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class GeneController : Controller
{
    private GeneService gc = new GeneService("admin", "admin123");
    
    [HttpGet("[action]")]
    public ActionResult GetAllGenes()
    {
        var test = gc.GetAsync();
        return Ok(gc.GetAsync());
    }

    [HttpGet("[action]/{id}")]
    public ActionResult GetGeneByGuid(Guid id)
    {
        return Ok();
    }
}