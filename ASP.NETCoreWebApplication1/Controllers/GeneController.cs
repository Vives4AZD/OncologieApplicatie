using Microsoft.AspNetCore.Mvc;
using OncologieApplicatie.Services;

namespace ASP.NETCoreWebApplication1.Controllers;

[ApiController]
[Route("[Controller]")]
public class GeneController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private GeneService gc = new GeneService("admin", "admin123");

    public GeneController(IHttpClientFactory httpClientFactory)
    { 
        GeneService.addClientFactory(httpClientFactory);

    }

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