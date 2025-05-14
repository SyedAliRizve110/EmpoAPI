using Empo.BuildingBlocks.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Empo.BuildingBlocks.Infrastructure;

public class UserInfoProvider : IUserInfoProvider
{
    private IHttpContextAccessor _contextAccessor;

    public UserInfoProvider(IHttpContextAccessor contextAccessor)
    {
            _contextAccessor = contextAccessor;
    }
    public Guid GetUserId()
    {
        var userId = "5cfa4250-9c36-4110-93e8-9b48f7805c36";
        var _httpContext = _contextAccessor.HttpContext;
        if (_httpContext != null)
        {
            if (_httpContext.Request.Headers.TryGetValue("Token", out var tokenId))
            {

                if (!string.IsNullOrEmpty(tokenId))
                {
                    // UserId Get from Help of Token
                    // this is future concept , how will be we get from token.
                    return new Guid(userId);
                }

            }

        }
        return new Guid(userId);
        //return Guid.Empty;
    }
}
