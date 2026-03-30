using System;
using System.Net.Http;

Console.Write("What do you want to check? (CI for internet connection, OS for operating system): ");
string Command = Console.ReadLine();
if (Command == "CI")
{
    bool isConnected = await IsConnectedToInternetAsync();
    Console.WriteLine(isConnected ? "Internet connection available" : "No internet connection");
}
else if (Command == "OS")
{
    Console.WriteLine(Environment.OSVersion);
}
else
{
    Console.WriteLine("Unknown command");
}

async Task<bool> IsConnectedToInternetAsync()
{
    try
    {
        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(5);
        var response = await client.GetAsync("http://google.com/generate_204");
        return response.IsSuccessStatusCode;
    }
    catch
    {
        return false;
    }
}

