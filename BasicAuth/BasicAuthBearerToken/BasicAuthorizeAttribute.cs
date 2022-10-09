using Microsoft.AspNetCore.Authorization;

namespace BasicAuthBearerToken
{

    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        public BasicAuthorizeAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}
