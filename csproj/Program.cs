using System;
using System.Net.Http;
using ITToolkit.Services;

var internetService = new InternetService();

Console.Write("What do you want to check? (CI for internet connection, OS for operating system): ");
var Command = Console.ReadLine();
if (Command == "CI")
{
    bool isConnected = await InternetService.IsConnectedToInternetAsync();
    Console.WriteLine(isConnected? "Internet connection available" : "No internet connection");
}
else if (Command == "OS")
{
    Console.WriteLine(Environment.OSVersion);
}
else
{
    Console.WriteLine("Unknown command");
}


