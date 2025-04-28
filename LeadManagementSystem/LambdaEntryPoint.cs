public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        //builder.UseStartup<Program>(); // Points to your Startup.cs

        builder
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Program>()
            .UseLambdaServer();
    }
}