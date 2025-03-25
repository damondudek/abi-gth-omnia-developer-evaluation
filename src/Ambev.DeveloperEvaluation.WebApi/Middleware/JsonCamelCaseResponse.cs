using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public static class JsonCamelCaseResponse
    {
        public static Task HandleResponseAsync(HttpContext context, int statusCode, object response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
