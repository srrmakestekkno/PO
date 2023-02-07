using System.Text.Json.Serialization;

namespace PowerOffice_1.DataObjects
{
    public class Naeringskode
    {
        [JsonPropertyName("kode")]
        public string? Code { get; set; }
    }
}
