using Microsoft.AspNetCore.Mvc;
using OncologieApplicatie.Services;

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
    public async Task<ActionResult> GetGeneByGuid(Guid id)
    {
        var filter = new Dictionary<string, string>()
        {
            { "_id", id.ToString() }
        };
        var test = await gc.FindAsync(filter);
        return Ok();
    }
}