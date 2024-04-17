using System.Net.Http;

namespace _1XBetParser
{
    public class CurlRequester
    {
        HttpClientHandler HttpClientHandler = new HttpClientHandler();
        HttpClient _httpClient;

        public CurlRequester()
        {
            _httpClient = new HttpClient(HttpClientHandler);
            HttpClientHandler.AutomaticDecompression = System.Net.DecompressionMethods.All;
        }

        public async Task<string> GetRequest(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }    
    }
}
