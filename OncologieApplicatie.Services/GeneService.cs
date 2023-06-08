using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace OncologieApplicatie.Services;

public class GeneService
{
    private HttpClient _httpClient;
    private const string URI = "https://fine-garlics-type.loca.lt/oncologie/";

    public GeneService(string username, string password)
    {
        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
        _httpClient.BaseAddress = new Uri(URI);
    }

    public async Task<string?> GetAsync(bool includeDocumentValues = true)
    {
        var response = await _httpClient.GetAsync(includeDocumentValues ? $"_all_docs?include_docs=true" : "_all_docs");
        if (!response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> FindAsync(Dictionary<string, string> data)
    {
        string selector = "{\"selector\" : ";
        foreach (var keyValuePair in data)
        {
            selector += "{" + $"\"{keyValuePair.Key}\" : \"{keyValuePair.Value}\"" + "}";
        }

        selector += "}";

        // _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json"); ;

        var payload = new { selector = new { _id = "996c29314d61f5f8882c1a08c400048e" } };
        var response = await _httpClient.PostAsJsonAsync("_find", payload);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }


    // TODO: create functionality to add a new file to db
    // /oncologie/{Guid}
    public async Task<string?> CreateAsync(Dictionary<string, string> data)
    {
        var pairedKeyValues = new Dictionary<string, string>();
        string guid = Guid.NewGuid().ToString();
        pairedKeyValues["id"] = guid;
        foreach (var keyValuePair in data)
        {
            pairedKeyValues[keyValuePair.Key] = keyValuePair.Value;
        }
        var requestBody = JsonSerializer.Serialize(new
        {
            Doc = pairedKeyValues
        });

        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"/{guid}", content);

        return await response.Content.ReadAsStringAsync();
    }

    /*public async Task<string?> BulkCreateAsync(string json)
    {
        if (!CheckIfValidJson(json))
        {
            return null;
        }

        
        
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

    }*/
}





