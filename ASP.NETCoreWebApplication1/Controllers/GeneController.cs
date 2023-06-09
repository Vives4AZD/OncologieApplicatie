using Microsoft.AspNetCore.Mvc;
using OncologieApplicatie.Services;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("[Controller]")]
public class GeneController : Controller
{
	//private GeneService gc = new GeneService("admin", "admin123");

	private GeneService gc = new GeneService("admin", "test");

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
            { "_id", id.ToString().Replace("-", "") }
        };
        var test = await gc.FindAsync(filter);
        return Ok(test);
    }


    [HttpPost("[action]")]
    public async Task<ActionResult> CreateGene([FromBody] Dictionary<string, string> data)
    {
	    var createdGene = await gc.CreateAsync(data);
	    if (createdGene == null)
	    {
		    return BadRequest("Failed to create the gene.");
	    }

	    return Ok(createdGene);
    }

}

