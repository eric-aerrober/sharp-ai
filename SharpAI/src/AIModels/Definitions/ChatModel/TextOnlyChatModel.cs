public class TextOnlyChatModel : AIModel<ChatContext, ChatContext>
{

    private CacheAsJsonOnDisk<ChatContext, ChatResult> _cache;

    public TextOnlyChatModel(string name, string provider, CacheAsJsonOnDisk<ChatContext, ChatResult> cache) : base(name, provider)
    {
        _cache = cache;
        _cache.ConfigureCachePath($"{provider}/{name}");
    }
    
    public override async Task<ModelResult<ChatContext, ChatContext>> Invoke(ChatContext input)
    {
        ChatResult outputResult;

        if (_cache.Has(input))
        {
            outputResult = _cache.Get(input);
        }

        else 
        {
            outputResult = await OnInvoke(input);
            _cache.Set(input, outputResult);
        }

        ChatContext resultContext = input.AddBotMessage(outputResult.message);
        return new ModelResult<ChatContext, ChatContext>(this, resultContext);
    }

    public virtual Task<ChatResult> OnInvoke(ChatContext input)
    {
        throw new NotImplementedException();
    }
}