namespace Aerrobert.Packages.SharpAI;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

public class AnthropicClaude3Point5TextOnlyChatModel : TextOnlyChatModel 
{

    private string _apiKey;
    private string _apiUrl = "https://api.anthropic.com/v1/messages";
    private string _modelId = "claude-3-5-sonnet-20240620";
    private HttpClient _client = new HttpClient();

    public AnthropicClaude3Point5TextOnlyChatModel(string apiKey, CacheAsJsonOnDisk<ChatContext, ChatResult> cache) : base("Claude3.5Chat", "Anthropic", cache)
    {
        _apiKey = apiKey;
    }

    private List<object> BuildChat (ChatContext input) {
        List<object> messages = new List<object>();
        
        foreach (ChatMessage message in input.messages) {
            messages.Add(new {
                role = message.sender == ChatSender.Bot ? "assistant" : "user",
                content = message.message
            });
        }

        return messages;
    }
    
    public override async Task<ChatResult> OnInvoke(ChatContext input)
    {

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _apiUrl);
        request.Headers.Add("x-api-key", _apiKey);
        request.Headers.Add("anthropic-version", "2023-06-01");

        string contentString = JsonSerializer.Serialize(new {
            model = _modelId,
            max_tokens = 2048,
            messages = BuildChat(input)
        });
        request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        return handleResponse(responseContent);
    
    }

    private ChatResult handleResponse (String response) {

        JObject responseJson = JObject.Parse(response);

        if (responseJson["type"].ToString() == "error") {
            throw new ModelInvokeError("Error from Anthropic API: " + response);
        }

        JArray content = (JArray) responseJson["content"];
        JObject firstContentElement = (JObject) content[0];
        return new ChatResult(firstContentElement["text"].ToString());

    }



}