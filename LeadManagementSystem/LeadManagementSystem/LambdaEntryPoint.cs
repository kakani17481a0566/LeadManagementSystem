using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.AspNetCoreServer.Internal;
using Amazon.Lambda.Core;

namespace LeadManagementSystem.LeadManagementSystem
{
    public class LambdaEntryPoint: APIGatewayHttpApiV2ProxyFunction
    {
        protected override void Init(IWebHostBuilder builder) => builder.UseStartup<Program>();

        protected override void MarshallRequest(
          InvokeFeatures features,
          APIGatewayHttpApiV2ProxyRequest apiGatewayRequest,
          ILambdaContext lambdaContext)
        {
            try
            {
                lambdaContext.Logger.LogLine($"Begin MarshallRequest");

                // Log individual properties to check for nulls
                lambdaContext.Logger.LogLine($"RequestContext: {JsonSerializer.Serialize(apiGatewayRequest?.RequestContext)}");
                lambdaContext.Logger.LogLine($"Headers: {JsonSerializer.Serialize(apiGatewayRequest?.Headers)}");
                lambdaContext.Logger.LogLine($"QueryStringParameters: {JsonSerializer.Serialize(apiGatewayRequest?.QueryStringParameters)}");
                lambdaContext.Logger.LogLine($"Body: {apiGatewayRequest?.Body}"); // Body might be large, consider logging only if needed and be mindful of sensitive data

                lambdaContext.Logger.LogLine($"InvokeFeatures: {JsonSerializer.Serialize(features)}");

                // Call base method to handle request marshalling
                base.MarshallRequest(features, apiGatewayRequest, lambdaContext);

                lambdaContext.Logger.LogLine($"End MarshallRequest");
            }
            catch (Exception ex)
            {
                lambdaContext.Logger.LogLine($"Error in MarshallRequest: {ex.Message}");
                lambdaContext.Logger.LogLine($"Stack Trace: {ex.StackTrace}");
                throw; // Optionally re-throw or handle the error
            }
        
    }
    }
}
