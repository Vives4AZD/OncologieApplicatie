using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OncologieApplicatie.Services;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("[Controller]")]
public class GeneController : Controller
{
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




	[HttpPut("[action]/{id}")]
	
	
	public async Task<ActionResult> UpdateGene(Guid id, [FromBody] Dictionary<string, string> updatedData)
	{
		var gene = await gc.FindAsync(new Dictionary<string, string> { { "_id", id.ToString().Replace("-", "") } });

		if (gene == null)
		{
			return NotFound();
		}

		// Get the revision from the gene document

		
		var geneObject = JsonSerializer.Deserialize<dynamic>(gene);
		string revision = geneObject.docs[0]["_rev"];

		Console.WriteLine(id);

		Console.WriteLine(revision);

		var updatedGene = await gc.UpdateAsync(id.ToString().Replace("-", ""), revision, updatedData);

		if (updatedGene == null)
		{
			return BadRequest();
		}

		return Ok(updatedGene);
	}



}