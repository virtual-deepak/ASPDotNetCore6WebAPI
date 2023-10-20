using System.Net;

public class ApiKeyAuthMiddleware
{
    private RequestDelegate _next;
    public ApiKeyAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration config)
    {
        var isHeaderKeyPresent = context.Request.Headers.ContainsKey("ApiKey");
        context.Request.Headers.TryGetValue("ApiKey", out var value);

        if(isHeaderKeyPresent)
        {
            if (value == config["ApiKey"])
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await _next(context); // Next handler
                return;
            }
        }
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        await context.Response.WriteAsync("Please authenticate before using the app");
        return; // Terminate the pipeline here
    }
}