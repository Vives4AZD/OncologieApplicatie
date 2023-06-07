using Microsoft.AspNetCore.Mvc;
using OncologieApplicatie.Core.Controllers.OncologieApplicatie.Services;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("[Controller]")]
public class GeneController : Controller
{
    private GeneService gc = new GeneService("admin", "admin123");
    
    [HttpGet("[action]")]
    public async Task<ActionResult> GetAllGenes()
    {
        var test = await  gc.GetAsync();
        return Ok(test);
    }

    [HttpGet("[action]/{id}")]
    public ActionResult GetGeneByGuid(Guid id)
    {
        return Ok();
    }
}