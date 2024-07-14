using Newtonsoft.Json.Linq;

public class ChatContext 
{

    public readonly List<ChatMessage> messages = new List<ChatMessage>();
    private readonly ChatSender _lastSender = ChatSender.Bot;

    public ChatContext()
    {

    }

    public ChatContext(List<ChatMessage> messages)
    {
        this.messages = messages;
    }

    private ChatContext AddMessage(ChatMessage message)
    {
        List<ChatMessage> new_messages = new List<ChatMessage>(messages);

        if (message.sender == _lastSender)
        {
            if (_lastSender == ChatSender.Bot) {
                new_messages.Add(new ChatMessage("Continue", ChatSender.Bot));
            } else {
                new_messages.Add(new ChatMessage("Continue", ChatSender.User));
            }
        }

        new_messages.Add(message);
        return new ChatContext(new_messages);
    }

    private ChatContext AddMessage(string message, ChatSender sender)
    {
        return AddMessage(new ChatMessage(message, sender));
    }

    public ChatContext AddUserMessage(string message)
    {
        return AddMessage(message, ChatSender.User);
    }

    public ChatContext AddBotMessage(string message)
    {
        return AddMessage(message, ChatSender.Bot);
    }

    public String LastMessage()
    {
        return messages.Last().message;
    }

    public String TruncatedLastMessage()
    {
        String lastMessage = LastMessage();
        if (lastMessage.Length > 120)
        {
            return lastMessage.Substring(0, 117) + "...";
        }
        return lastMessage;
    }

    public JObject LastObject () {

        string lastMessage = LastMessage();
        int firstCurly = lastMessage.IndexOf("{");
        int lastCurly = lastMessage.LastIndexOf("}");

        if (firstCurly == -1 || lastCurly == -1)
        {
            return new JObject();
        }

        string jsonString = lastMessage.Substring(firstCurly, lastCurly - firstCurly + 1);
        return JObject.Parse(jsonString);

    }

    public string ReadString (String path) {
        return LastObject().SelectToken(path).ToString();
    }

    public override string ToString()
    {
        return messages.Select(m => m.message).Aggregate((a, b) => a + b);
    }

}