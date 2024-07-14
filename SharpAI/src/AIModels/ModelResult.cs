public class ModelResult <IInput, IOutput>
{
    public readonly AIModel<IInput, IOutput> model;

    public readonly IOutput result;

    public ModelResult(AIModel<IInput, IOutput> model, IOutput result)
    {
        this.model = model;
        this.result = result;
    }
}