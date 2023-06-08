﻿using System.Net.Http.Json;
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
        var selector = new Dictionary<string, string>();
        foreach (var keyValuePair in data)
        {
            selector.Add(keyValuePair.Key, keyValuePair.Value);
        }

        var payload = new { selector };
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
        var docs = new Dictionary<string, string>();
        string guid = Guid.NewGuid().ToString().Replace("-", "");
        foreach (var keyValuePair in data)
        {
            docs.Add(keyValuePair.Key, keyValuePair.Value);
        }

        var payload = new { docs };
        var response = await _httpClient.PostAsJsonAsync($"/{guid}", payload);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

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





