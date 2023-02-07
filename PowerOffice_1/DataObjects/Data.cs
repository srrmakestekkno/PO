using System.Text.Json.Serialization;

namespace PowerOffice_1.DataObjects
{
    public class Data
    {
        [JsonPropertyName("organisasjonsnummer")]
        public string? OrganizationNumber { get; set; }

        [JsonPropertyName("antallAnsatte")]
        public int NumberOfEmployees { get; set; }

        [JsonPropertyName("naeringskode1")]
        public Naeringskode Naeringskode { get; set; }

        [JsonPropertyName("organisasjonsform")]
        public OrganizationForm OrganizationForm { get; set; }

        [JsonPropertyName("navn")]
        public string? BrrRegName { get; set; }

        [JsonPropertyName("konkurs")]
        public bool IsBankrupt { get; set; }

        public string? Name { get; set; }
        public bool IsOk { get; set; } = true;
        public int StatusCode { get; set; }
    }
}
