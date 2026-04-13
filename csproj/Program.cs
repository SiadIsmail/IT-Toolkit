using System;
using System.Net.Http;
using ITToolkit.Commands;
using ITToolkit.Services;

var internetService = new InternetService();

Console.Write("What do you want to check? (CI for internet connection, OS for system details): ");
var Command = Console.ReadLine();
if (Command == "CI")
{
    bool isConnected = await InternetService.IsConnectedToInternetAsync();
    Console.WriteLine(isConnected ? "Internet connection available" : "No internet connection");
}
else if (Command == "OS")
{
    SystemInformation.PrintSystemSummary();
}
else if (Command == "Test")
{
    
}
else
{
    Console.WriteLine("Unknown command");
}