using Empo.BuildingBlocks.Application;

namespace Empo.EmloyeeService.Api.Configuration
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid CorrelationId
        {
            get
            {
                if (IsAvailable && _httpContextAccessor.HttpContext.Response.Headers.Keys.Any(x => x == CorrelationMiddleware.CorrelationIdHeaderKey))
                {
                    return Guid.Parse(_httpContextAccessor.HttpContext.Request.Headers[CorrelationMiddleware.CorrelationIdHeaderKey]);
                }
                throw new ApplicationException("Http contextt and correlation id is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
        public Guid UserId => throw new NotImplementedException();
    }
}
