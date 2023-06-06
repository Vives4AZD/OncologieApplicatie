
using System.Text;

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

	public async Task<string> GetAsync()
	{
        var response = await _httpClient.GetAsync($"{URI}/_bulk_get");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
		return await response.Content.ReadAsStringAsync();
    }

	public async Task<string> PostRequest(Dictionary<string, string> data)
	{
		var contentType = "application/json";

		var requestBody = $@"{
            ""selector"": {
                ""_id"": ""ee929a186e286257678a0358850039a0""
            }
        }";

		HttpClient httpClient = new HttpClient();
		httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encodedCredentials);
		httpClient.DefaultRequestHeaders.Add("Referer", referer);

		var content = new StringContent(requestBody, Encoding.UTF8, contentType);

		var response = await httpClient.PostAsync($"https://{referer}/oncologie/_find", content);
		response.EnsureSuccessStatusCode();

		var responseBody = await response.Content.ReadAsStringAsync();

		Console.WriteLine(responseBody);
	}




	//TODO: function that gets all the data from the database



}





