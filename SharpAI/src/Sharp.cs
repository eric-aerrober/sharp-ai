using Microsoft.Extensions.Logging;

namespace sharp_ai;


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
