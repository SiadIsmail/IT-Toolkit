using System;
using System.Net.Http;

namespace ITToolkit.Services
{
    public class InternetService
    {
        public static async Task<bool> IsConnectedToInternetAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(5);
                    var response = await httpClient.GetAsync("http://www.google.com");
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}