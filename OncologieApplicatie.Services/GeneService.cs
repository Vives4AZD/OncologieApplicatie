﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace OncologieApplicatie.Services;

public class GeneService
{
    private HttpClient _httpClient;
    private const string URI = "http://localhost:5984/oncologiedatabase/";

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
    /// <returns>All the documents as a json string.</returns>
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
    public async Task<string?> FindAsync(Dictionary<string, object> data)
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
    /// <param name="docPayload">
    /// The payload data for the document. The keys are the names of the variables (ex: Gene) and the value is the value of said variable.
    /// This method adds an id automatically, do not add it manually.
    /// </param>
    /// <returns>The created document as a json string.</returns>
    public async Task<string?> CreateAsync(Dictionary<string, object> docPayload)
    {
        // Generate a new unique id for the document
        var guid = Guid.NewGuid().ToString().Replace("-", "");
        
        if (docPayload.ContainsKey("_id"))
        {
            docPayload.Remove("_id");
        }

        if (docPayload.ContainsKey("_rev"))
        {
            docPayload.Remove("_rev");
        }

        docPayload.Add("_id", guid);

        // Send a POST request with the payload to create the new document
        var response = await _httpClient.PostAsJsonAsync($"", docPayload);

        // If the request was not successful, return null
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        // Otherwise, find and return the newly created document
        return await FindAsync(new Dictionary<string, object>(){ { "_id", guid } });
    }

    /// <summary>
    /// Updates an existing document in the database.
    /// </summary>
    /// <param name="id">The id of the document that needs to be updated.</param>
    /// <param name="updateData">The data to update the existing object with.</param>
    /// <returns></returns>
    public async Task<string?> UpdateAsync(string id, Dictionary<string, object?> updatePayload)
    {
        var response = await _httpClient.PutAsJsonAsync($"{id}", updatePayload!);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await FindAsync(new Dictionary<string, object>() { { "_id", id } });
    }



    //currently unused!!
    /// <summary>
    /// When uploading a tsv or csv file, this will push all the records in said file to the db
    /// </summary>
    /// <param name="svData">The stream gotten from the angular side (unfinished)</param>
    /// <param name="extension">The extension of the file. Has to be either .csv or .tsv</param>
    /// <returns>A boolean where true means that the server has added the documents.</returns>
    /// <exception cref="Exception">Throws an exception if the filetype is unsupported</exception>
    public async Task<bool> CreateBulkAsync(Stream svData, string extension)
    {
        if (!extension.StartsWith('.'))
        {
            extension = '.' + extension;
        }

        string? header = null;
        string[] keys = new string[] { };
        List<Dictionary<string, object>> bulkDocs = new List<Dictionary<string, object>>();
        string? separator = extension switch
        {
            ".csv" => ",",
            ".tsv" => "\t",
            _ => throw new Exception("Unsupported filetype.")
        };

        using (var reader = new StreamReader(svData))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (header == null)
                {
                    header = line;
                    keys = header.Split(separator);
                    continue;
                }

                string[] values = line.Split(separator);
                var docData = new Dictionary<string, object>();
                for (int i = 0; i < keys.Length; i++)
                {
                    ((IDictionary<string, object>)docData).Add(keys[i], values[i]);
                }

                bulkDocs.Add(docData);
            }
        }

        var payload = new { docs = bulkDocs };

        var response = await _httpClient.PostAsJsonAsync($"_all_docs", payload);

        return response.IsSuccessStatusCode;
    }
}