
using System.Text;
using System.Text.Json;

namespace OncologieApplicatie.Core.Controllers;

public class GeneController
{
    private string _password;
    private string _username;
	private HttpClient _httpClient;
	private const string URI = "https://slimy-aliens-hope.loca.lt/oncologie";

    public GeneController(string username, string password)
    {
        _username = username; //"admin";
        _password = password; //"admin123";
        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(_username + ":" + _password));
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
    }

	public async Task<string?> GetAsync()
	{
        var response = await _httpClient.GetAsync($"{URI}/_bulk_get");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
		return await response.Content.ReadAsStringAsync();
    }

	public async Task<string?> FindAsync(Dictionary<string, string> data)
	{
		var filter = new Dictionary<string, string>();
        foreach (var keyValuePair in data)
        {
            filter[keyValuePair.Key] = keyValuePair.Value;
        }

        var requestBody = JsonSerializer.Serialize(new
        {
            Selector = filter
        });

        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

		var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

		var response = await _httpClient.PostAsync($"{URI}/_find", content);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
	}


    // TODO: create functionality to add a new file to db
    // /oncologie/{Guid}
    public async Task<string?> CreateAsync(string json)
    {
        if (!CheckIfValidJson(json))
        {
            return null;
        }
        
        var requestBody = JsonSerializer.Serialize(new
        {
            Doc = JsonSerializer.Serialize(json)
        });

        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        Guid id = new Guid();

        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{URI}/", content);

        return await response.Content.ReadAsStringAsync();
    }

    // TODO: create functionality to add bulk new files to db
    // /oncologie/_bulk_docs
    public async Task<string?> BulkCreateAsync(string json)
    {
        if (!CheckIfValidJson(json))
        {
            return null;
        }

        
        
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

    }

    private static bool CheckIfValidJson(string json)
    {
        try
        {
            var obj = JsonSerializer.Deserialize<object>(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}





