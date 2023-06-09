using System.Dynamic;
using System.Text.Json;
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
        var filter = new Dictionary<string, object>()
        {
            { "_id", id.ToString().Replace("-", "") }
        };
        var test = await gc.FindAsync(filter);
        return Ok(test);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> CreateGenePosition([FromBody] ExpandoObject body)
    {
        var myObject = new Dictionary<string, object?>(body);
        var test = await gc.CreateAsync(myObject);
        
        return Ok(test);
    }

    [HttpPost("[action]/{id}")]
    public async Task<ActionResult> UpdateGenePosition([FromRoute] Guid id, [FromBody] ExpandoObject body)
    {
        var myObject = new Dictionary<string, object?>(body);
        var test = await gc.UpdateAsync(id.ToString().Replace("-",""), myObject);
        return Ok(test);
    }
}