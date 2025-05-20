using Empo.Shared.Utility.Constants;

namespace Empo.ApiGateway.Configurations;

public class HeaderDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var tenantId = Constant.DefaultTenantId.ToString();
        if (request.Headers.Contains("tenant-id") == false)
            request.Headers.Add("tenant-id", tenantId);

        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }
}
