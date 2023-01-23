using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;

namespace  TryKatchOPENAI;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Ask me about Any DevOps commands!");
        var question = Console.ReadLine();

        var response = callOpenAI(250, question, "text-davinci-002", 0.7, 1, 0, 0);
        Console.WriteLine($"Your command is : {response}");
        Console.WriteLine("Press any key to exit the process");
        Console.ReadKey(true);
        Environment.Exit(0);
    }

    private static string callOpenAI(int tokens, string input, string model,
        double temperature, int topP, int frequencyPenalty, int presencePenalty)
    {

        var apiKey = "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";
        
        var apiUrl = "https://api.openai.com/v1/engines/" + model + "/completions";

        try
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), apiUrl))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + apiKey);
                    request.Content = new StringContent("{\n  \"prompt\": \"" + input + "\",\n  \"temperature\": " +
                                                        temperature.ToString(CultureInfo.InvariantCulture) + ",\n  \"max_tokens\": " + tokens + ",\n  \"top_p\": " + topP +
                                                        ",\n  \"frequency_penalty\": " + frequencyPenalty + ",\n  \"presence_penalty\": " + presencePenalty + "\n}");

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.SendAsync(request).Result;
                    var json = response.Content.ReadAsStringAsync().Result;

                    dynamic dynObj = JsonConvert.DeserializeObject(json) ?? throw new InvalidOperationException();

                    if (dynObj != null)
                    {
                        return dynObj.choices[0].text.ToString();
                    }


                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        
        
        return null;
    }
}

