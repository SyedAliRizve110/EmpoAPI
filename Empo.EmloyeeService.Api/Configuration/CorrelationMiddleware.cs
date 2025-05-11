namespace Empo.EmloyeeService.Api.Configuration
{
    internal class CorrelationMiddleware
    {
        internal const string CorrelationIdHeaderKey = "CorrelationId";
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context
            )
        {
            var correlationId = Guid.NewGuid();
            if (context.Request !=null)
            {
                context.Request.Headers.Add(CorrelationIdHeaderKey, correlationId.ToString());
            }
            await this._next.Invoke(context);
        }
    }
}
