using BLL.Services.Bases;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    public class HttpService : HttpServiceBase
    {
        public HttpService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}
