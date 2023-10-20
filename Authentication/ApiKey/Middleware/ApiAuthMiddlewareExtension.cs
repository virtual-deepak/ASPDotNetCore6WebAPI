public static class ApiAuthMiddlewareExtension
{
    public static IApplicationBuilder UseApiKey(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ApiKeyAuthMiddleware>();
    }
}