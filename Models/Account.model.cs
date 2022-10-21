using Newtonsoft.Json;

namespace linksnews2API.Models;

public class Account
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("pages")]
    public List<Page> Pages { get; set; }
}
