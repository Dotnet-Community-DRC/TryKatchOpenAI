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

        var response = OpenAiApi.CallOpenAi(250, question, "text-davinci-002", 0.7, 1, 0, 0);
        Console.WriteLine($"Your command is : {response}");
        Console.WriteLine("Press any key to exit the process");
        Console.ReadKey(true);
        Environment.Exit(0);
    }
}

