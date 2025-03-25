namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public static class MiddlewareIoC
    {
        public static void UseMiddlewareIoC(this WebApplication app)
        {
            app.UseMiddleware<ValidationExceptionMiddleware>();
            app.UseMiddleware<UnauthorizedExceptionMiddleware>();
            app.UseMiddleware<ArgumentExceptionMiddleware>();
            app.UseMiddleware<KeyNotFoundExceptionMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
