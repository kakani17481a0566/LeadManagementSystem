using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;

namespace LeadManagementSystem.ScratchToCode
{
    public class LambdaFunction : APIGatewayHttpApiV2ProxyFunction
    {
        public APIGatewayHttpApiV2ProxyResponse FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
        {
            try
            {
                if (request == null)
                {
                    context.Logger.LogLine("API Gateway Request is null.");
                    return new APIGatewayHttpApiV2ProxyResponse
                    {
                        StatusCode = 400,
                        Body = "Bad Request"
                    };
                }

                // Log important properties of the request for debugging
                context.Logger.LogLine($"Request Path: {request.RawPath}");
                context.Logger.LogLine($"Request Headers: {JsonSerializer.Serialize(request.Headers)}");

                // Safely check for null properties
                if (request.Headers == null)
                {
                    context.Logger.LogLine("Headers are null.");
                }

                // Handle your business logic here
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = 200,
                    Body = "Success"
                };
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                context.Logger.LogLine($"StackTrace: {ex.StackTrace}");

                // Return an error response
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = 500,
                    Body = $"Internal Server Error: {ex.Message}"
                };
            }
        
        }
    
            
    }
}
