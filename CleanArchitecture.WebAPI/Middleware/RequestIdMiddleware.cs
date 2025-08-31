namespace CleanArchitecture.WebAPI.Middleware
{
    public class RequestIdMiddleware
    {
        private const string HeaderKey = "X-Request-Id";
        private readonly RequestDelegate _next;

        public RequestIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string requestId;

            if (context.Request.Headers.TryGetValue(HeaderKey, out var headerValue) && !string.IsNullOrWhiteSpace(headerValue))
            {
                requestId = headerValue.ToString();
            }
            else
            {
                requestId = Guid.NewGuid().ToString();
            }

            context.Items[HeaderKey] = requestId;

            context.Response.OnStarting(() =>
            {
                context.Response.Headers[HeaderKey] = requestId;
                return Task.CompletedTask;
            });

            using (Serilog.Context.LogContext.PushProperty("RequestId", requestId))
            {
                await _next(context);
            }
        }
    }
}
