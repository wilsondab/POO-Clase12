using System.Text.Json.Serialization;

public class Login
{
    [JsonPropertyName("username")]
    public string userName { get; set; }

    public string password { get; set; }
}