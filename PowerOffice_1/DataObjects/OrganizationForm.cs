using System.Text.Json.Serialization;

namespace PowerOffice_1.DataObjects
{
    public class OrganizationForm
    {
        [JsonPropertyName("kode")]
        public string? Code { get; set; }
    }
}
