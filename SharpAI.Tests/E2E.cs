using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using sharp_ai;

namespace SharpAI.Tests;

public class E2ETests
{
    [Fact]
    public async void TestAnthropic()
    {

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "hh:mm:ss ";
            });
            builder.SetMinimumLevel(LogLevel.Information);
        });
        var logger = loggerFactory.CreateLogger("SharpAI");

        Sharp sharp = new Sharp {
            logger = logger,
            modelInterface = new ModelAccessor {
                chatModel = new AnthropicClaude3Point5TextOnlyChatModel (
                    apiKey: "",
                    cache: new CacheAsJsonOnDisk<ChatContext, ChatResult>()
                )
            }
        };

        SharpExecutionContext ctx = sharp.Begin("AnthropicTest");
        
        await ctx.AskAITyped(
            """
                Hi, I'm a test message. Please respond with three words.
            """,
            new {
                thoughts = "your inner thoughts",
                response = "response string"
            }
        );

        string obj = ctx.chat.ReadString("response");


    }
}