using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace OncologieApplicatie.Services;

public class GeneService
{
    private HttpClient _httpClient;
    private const string URI = "https://real-baths-beg.loca.lt/oncologie/";

    public GeneService(string username, string password)
    {
        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
        _httpClient.BaseAddress = new Uri(URI);
    }

    /// <summary>
    /// Gets all the documents from the database.
    /// </summary>
    /// <param name="includeDocumentValues">A flag indicating whether to include document values in the response.</param>
    /// <returns>All the documents as a JSON string.</returns>
    public async Task<string?> GetAsync(bool includeDocumentValues = true)
    {
        // Send an HTTP GET request to the _all_docs endpoint with the include_docs parameter set to true/false based on the value of includeDocumentValues
        var response = await _httpClient.GetAsync(includeDocumentValues ? $"_all_docs?include_docs=true" : "_all_docs");

        // If the response was not successful, return the error message as a string
        if (!response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        // Otherwise, return the response content as a string
        return await response.Content.ReadAsStringAsync();
    }


    /// <summary>
    /// Finds one (or more) documents in the database.
    /// </summary>
    /// <param name="data">The data on which to find. The keys are the names of the filter (ex: _id) and the value is the value of said filter.</param>
    /// <returns>The found document(s) as a json string.</returns>
    public async Task<string?> FindAsync(Dictionary<string, string> data)
    {
        // Create a payload object with a selector property that contains the data dictionary
        var payload = new { selector = data };

        // Send a POST request with the payload to the "_find" endpoint using the HttpClient
        var response = await _httpClient.PostAsJsonAsync("_find", payload);

        // If the response is not successful, return null
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        // Otherwise, read the response content as a string and return it
        return await response.Content.ReadAsStringAsync();
    }


    /// <summary>
    /// Creates a new document in the database.
    /// </summary>
    /// <param name="data">
    /// The data for the document. The keys are the names of the variables (ex: _id) and the value is the value of said variable.
    /// </param>
    /// <returns>The created document.</returns>
    public async Task<string?> CreateAsync(Dictionary<string, string> data)
    {
        // Generate a new unique id for the document
        var guid = Guid.NewGuid().ToString().Replace("-", "");

        // Construct the payload object with the new id and the document data
        var payload = new { _id = guid, doc = data };

        // Send a POST request with the payload to create the new document
        var response = await _httpClient.PostAsJsonAsync($"/{guid}", payload);

        // If the request was not successful, return null
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        // Otherwise, find and return the newly created document
        return await FindAsync(new Dictionary<string, string>(){ { "_id", guid } });
    }
}