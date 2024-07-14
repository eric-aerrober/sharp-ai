namespace Aerrobert.Packages.SharpAI;

public class AIModel <IInput, IOutput>
{

    public readonly string name;
    public readonly string provider;

    public AIModel(string name, string provider)
    {
        this.name = name;
        this.provider = provider;
    }

    public virtual Task<ModelResult<IInput, IOutput>> Invoke(IInput input)
    {
        throw new NotImplementedException();
    }
}