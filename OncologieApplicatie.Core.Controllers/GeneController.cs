
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

	//TODO: function that gets one data from the database, write http request

	public async Task<HttpContent> GetAsync()
	{
        HttpResponseMessage response = await _httpClient.GetAsync($"{URI}/_bulk_get");
		response.EnsureSuccessStatusCode();
		var responseBody = response.Content;
    }

	public async Task PostRequest()
	{

		var username = "admin";
		var password = "admin123";
		var referer = "all-islands-think.loca.lt";
		var contentType = "application/json";

		var requestBody = @"{
            ""selector"": {
                ""_id"": ""ee929a186e286257678a0358850039a0""
            }
        }";

		var encodedCredentials = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
			.GetBytes(username + ":" + password));

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





