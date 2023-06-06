
using System.Text;

namespace OncologieApplicatie.Core.Controllers;

public class GeneController
{


	//TODO: function that gets one data from the database, write http request

	public static async Task GetAsync(HttpClient httpClient)
	{

		var username = "admin";
		var password = "admin123";
		string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
			.GetBytes(username + ":" + password));


		HttpClient client = new HttpClient();

		client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);


		HttpResponseMessage response = await client.GetAsync("https://slimy-aliens-hope.loca.lt/oncologie/_bulk_get");
		response.EnsureSuccessStatusCode();
		string responseBody = await response.Content.ReadAsStringAsync();

		

	}

	public static async Task PostRequest()
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





