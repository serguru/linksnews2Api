using Newtonsoft.Json;

namespace linksnews2API.Models;

public class Column
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("links")]
    public List<Link> Links { get; set; }
}
