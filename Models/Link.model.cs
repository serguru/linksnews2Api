using Newtonsoft.Json;

namespace linksnews2API.Models;
public class Link
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("url")]
    public string Url { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
}
