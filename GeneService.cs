
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

	public async Task<string?> Find(Dictionary<string, string> data)
	{
		var contentType = "application/json";

		var filter = new Dictionary<string, string>();
        foreach (var keyValuePair in data)
        {
            filter[keyValuePair.Key] = keyValuePair.Value;
        }

        var requestBody = JsonSerializer.Serialize(new
        {
            Selector = filter
        });

        _httpClient.DefaultRequestHeaders.Add("Content-Type", contentType);

		var content = new StringContent(requestBody, Encoding.UTF8, contentType);

		var response = await _httpClient.PostAsync($"{URI}/_find", content);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
	}
}





