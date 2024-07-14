namespace Aerrobert.Packages.SharpAI;

using System.Text.Json;
using Microsoft.Extensions.Logging;

public class SharpExecutionContext {

    public required string name;
    public required ModelAccessor modelInterface;
    public required ILogger logger;
    public ChatContext chat = new ChatContext();

    public async Task AskAI (string message) {

        ChatContext withUserMessage = chat.AddUserMessage(message);
        logger.LogInformation("[{name}] is asking model '{model}' context: '{message}'", 
            name, modelInterface.chatModel.name, withUserMessage.TruncatedLastMessage());

        ModelResult<ChatContext, ChatContext> aiCallResult = await modelInterface.chatModel.Invoke(withUserMessage);
        logger.LogInformation("[{name}] got response from model '{model}' context: '{message}'", 
            name, modelInterface.chatModel.name, aiCallResult.result.TruncatedLastMessage());

        chat = aiCallResult.result;
    }

    public Task AskAITyped (string message, object type) {

        string typeString = JsonSerializer.Serialize(type);

        String typeMessage = """

            For the above message, respond in a valid JSON string matching the below type:

            """ + typeString + """

        """;

        return AskAI(message + typeMessage);

    }

}