namespace Aerrobert.Packages.SharpAI;

using Microsoft.Extensions.Logging;

public class Sharp
{

    public required ModelAccessor modelInterface;
    public required ILogger logger;
    
    public SharpExecutionContext Begin (String name) {

        logger.LogInformation("Starting sharp-ai execution {name}", name);

        return new SharpExecutionContext {
            name = name,
            modelInterface = modelInterface,
            logger = logger
        };  
    }

}
