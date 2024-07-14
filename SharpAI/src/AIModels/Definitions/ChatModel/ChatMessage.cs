public class ChatMessage 
{
    public readonly string message;
    public readonly ChatSender sender;

    public ChatMessage(string message, ChatSender sender)
    {
        this.message = message;
        this.sender = sender;
    }

}