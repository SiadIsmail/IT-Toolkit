using System;
using System.Net.Http;
using System.Threading.Tasks;
string Command = Console.ReadLine();
if (Command == "CI");
{
    
    
      static async Task Main(string[] args) {
        bool isConnected = await IsConnectedToInternetAsync();
        Console.WriteLine(isConnected ? "Internet connection available" : "No internet connection");
     }

       static async Task<bool> IsConnectedToInternetAsync() {
        try {
            using (var client = new HttpClient()) {
                client.Timeout = TimeSpan.FromSeconds(5);
                var response = await client.GetAsync("http://google.com/generate_204");
                return response.IsSuccessStatusCode;
            }
        }
        catch {
            return false;
        }

    }
    
}

