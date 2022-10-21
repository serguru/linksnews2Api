using Newtonsoft.Json;

namespace linksnews2API.Models;

public class Row
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("columns")]
    public List<Column> Columns { get; set; }
}
