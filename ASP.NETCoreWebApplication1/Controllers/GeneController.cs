using System.Dynamic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OncologieApplicatie.Services;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("[Controller]")]
public class GeneController : Controller
{
    private GeneService _geneService = new GeneService("admin", "admin123");
    
    [HttpGet("[action]")]
    public async Task<ActionResult> GetAllGenes()
    {
        // Call the GetAsync method from the geneservice
        var genes = await _geneService.GetAsync();

        // Returns an HTTP 200 (OK) response with the test result as the response body
        return Ok(genes);
    }


    [HttpGet("[action]/{id}")]
    public async Task<ActionResult> GetGeneByGuid(Guid id)
    {
        // Create a filter based on the provided Guid ID
        var filter = new Dictionary<string, object>()
        {
            { "_id", id.ToString().Replace("-", "") }
        };

        // Call the geneservice's FindAsync method with the filter
        var test = await _geneService.FindAsync(filter);

        // Returns an HTTP 200 (OK) response with the test result as the response body
        return Ok(test);
    }


    [HttpPost("[action]")]
    public async Task<ActionResult> CreateGenePosition([FromBody] ExpandoObject body)
    {
        // Convert the dynamic ExpandoObject to a Dictionary<string, object?>
        var myObject = new Dictionary<string, object?>(body);

        // Call the CreateAsync method from the GeneService with the dictionary as a parameter
        var result = await _geneService.CreateAsync(myObject);

        // Returns an HTTP 200 (OK) response with the test result as the response body
        return Ok(result);
    }

    [HttpPost("[action]/{id}")]
    public async Task<ActionResult> UpdateGenePosition([FromRoute] Guid id, [FromBody] ExpandoObject body)
    {
        // Creates a new dictionary from the ExpandoObject instance
        var myObject = new Dictionary<string, object?>(body);

        // Calls the UpdateAsync method of the geneservice instance and passes the dictionary and the id parameter as arguments
        var test = await _geneService.UpdateAsync(id.ToString().Replace("-",""), myObject);

        // Returns an HTTP 200 (OK) response with the test result as the response body
        return Ok(test);
    }
}