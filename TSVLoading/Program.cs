using System;
using System.Dynamic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("TSV Console app");
        //bool run = true;
        //do
        //{
            string? filePath = null;
            while (filePath == null)
            {
                Console.Write("Enter the path to the file:");
                filePath = Console.ReadLine();
            }

        /*string? outputPath = null;
        while (outputPath == null)
        {
            Console.Write("Enter the path for the output directory:");
            outputPath = Console.ReadLine();
        }*/

        string header = null, json;
        string[] keys = new string[] { };
        List<ExpandoObject> bulkDocs = new List<ExpandoObject>();
        var extension = Path.GetExtension(filePath);
        string? separator = extension switch
        {
            ".csv" => ",",
            ".tsv" => "\t",
            _ => throw new Exception("Unsupported filetype.")
        };

        foreach (var line in File.ReadAllLines(filePath))
        {

            if (line != null && header == null)
            {
                header = line;
                keys = header!.Split('\t');
                continue;
            }

            string[] values = line.Split('\t');
            var docData = new ExpandoObject();
            for (int i = 0; i < keys.Length; i++)
            {
                ((IDictionary<string, object>)docData).Add(keys[i], values[i]);
            }
            bulkDocs.Add(docData);
        }

        Console.WriteLine(JsonSerializer.Serialize(bulkDocs));



        /*using (StreamReader sr = new StreamReader(filePath))
        {
            List<ExpandoObject> jsons = new List<ExpandoObject>();
            string header = sr.ReadLine()!;
            string[] keys = header!.Split(separator);
            string line;
            int record = 1;
            string json, fileName, output;
            while ((line = sr.ReadLine()!) != null)
            {
                string[] values = line.Split('\t');
                var data = new ExpandoObject();
                for (int i = 0; i < keys.Length; i++)
                {
                    ((IDictionary<string, object>)data).Add(keys[i], values[i]);
                }

                jsons.Add(data);
                string gene = ((IDictionary<String, Object>)data)["Gene"].ToString();
                json = JsonSerializer.Serialize(data);
                fileName = $"{gene}_record{record}.json";
                output = Path.Combine(outputPath, fileName);

                File.WriteAllText(output, json);
                record++;

                Console.WriteLine($"{fileName} written successfully.");
            }

            json = JsonSerializer.Serialize(jsons);
            fileName = $"full_record.json";
            output = Path.Combine(outputPath, fileName);

            File.WriteAllText(output, json);

            Console.WriteLine($"{fileName} written successfully.");
        }

        } while (run);*/
    }
}