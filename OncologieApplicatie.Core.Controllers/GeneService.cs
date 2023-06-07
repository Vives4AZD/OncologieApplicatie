using System.Text;
using System.Text.Json;

namespace OncologieApplicatie.Services
{
    // This class represents a service for interacting with a gene database.
    public class GeneService
    {
        private HttpClient _httpClient;
        private const string URI = "https://slimy-aliens-hope.loca.lt/oncologie";

        public GeneService(string username, string password)
        {
            // Encode the username and password as a base64 string.
            // for localtunnel: admin:admin123
            string encoded =
                System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

            // Initialize the HttpClient with the encoded credentials and the base URI.
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
            _httpClient.BaseAddress = new Uri(URI);
        }

        public async Task<string?> GetAsync(bool includeDocumentValues = true)
        {
            // Create a GET request with the specified URL and include document values if requested.
            var response =
                await _httpClient.GetAsync(includeDocumentValues ? "/_bulk_get?include_docs=true" : "/_bulk_get");

            // If the response was unsuccessful, return null.
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            // Return the response body as a string.
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string?> FindAsync(Dictionary<string, string> data)
        {
            // Convert the dictionary of search criteria to a JSON object.
            var filter = new Dictionary<string, string>();
            foreach (var keyValuePair in data)
            {
                filter[keyValuePair.Key] = keyValuePair.Value;
            }

            // Construct the request body as a JSON object containing the search criteria.
            var requestBody = JsonSerializer.Serialize(new
            {
                Selector = filter
            });

            // Set the Content-Type header to "application/json".
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            // Create a new StringContent object containing the request body.
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Make a POST request to the gene database.
            var response = await _httpClient.PostAsync($"/_find", content);

            // If the response was unsuccessful, return null.
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            // Return the response body as a string.
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string?> CreateAsync(Dictionary<string, string> data)
        {
            // Create a new dictionary of key-value pairs to store in the new document, including a randomly generated GUID as the document ID.
            var pairedKeyValues = new Dictionary<string, string>();
            string guid = Guid.NewGuid().ToString();
            pairedKeyValues["id"] = guid;
            foreach (var keyValuePair in data)
            {
                pairedKeyValues[keyValuePair.Key] = keyValuePair.Value;
            }

            // Construct the request body as a JSON object containing the new document.
            var requestBody = JsonSerializer.Serialize(new
            {
                Doc = pairedKeyValues
            });

            // Set the Content-Type header to "application/json".
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            // Create a new StringContent object containing the request body.
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Make a POST request to the gene database with the GUID as the document ID.
            var response = await _httpClient.PostAsync($"/{guid}", content);

            // Return the response body as a string.
            return await response.Content.ReadAsStringAsync();
        }
    }
}
