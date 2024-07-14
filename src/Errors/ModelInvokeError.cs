namespace Aerrobert.Packages.SharpAI;

public class ModelInvokeError : Exception
{
    public ModelInvokeError(string message) : base(message)
    {
    }
}