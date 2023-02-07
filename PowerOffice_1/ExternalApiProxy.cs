using PowerOffice_1.DataObjects;
using System.Text.Json;

namespace PowerOffice_1
{
    public class ExternalApiProxy : IExternalApiProxy
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public async Task<Data?> GetAsync(string orgno)
        {
            Data? data = new();

            var response = await _httpClient.GetAsync($"https://data.brreg.no/enhetsregisteret/api/enheter/{orgno}");
            
            if (!response.IsSuccessStatusCode)
            {
                data.IsOk = false;
                data.StatusCode = (int)response.StatusCode;
            }
            else
            {
                data = await GetDeserializedData(response.Content);
            }            
                        
            return data;
        }

        private async Task<Data?> GetDeserializedData(HttpContent httpContent)
        {            
            Data? data = new();

            string json = await httpContent.ReadAsStringAsync();
            if (json != null)
            {
                data = JsonSerializer.Deserialize<Data>(json);
            }

            return data;
        }
    }
}
