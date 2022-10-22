using Newtonsoft.Json;

namespace linksnews2API.Models;

public class Login
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
}
