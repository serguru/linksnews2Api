using Newtonsoft.Json;
namespace linksnews2API.Models;

public class Page
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("rows")]
    public List<Row> Rows { get; set; }
}
