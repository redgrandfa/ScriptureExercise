using Common.Repositories;
using Microsoft.AspNetCore.Http;

namespace ScriptureExercise.Services
{
    public class BaseService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IMemoryCacheRepository _cacheRepo;
        protected BaseService(IHttpContextAccessor httpContextAccessor, IMemoryCacheRepository cacheRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheRepo = cacheRepo;
        }

        public int GetCurrentMemberId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
        }
    }

    //public class BaseService_withMember { }
}
