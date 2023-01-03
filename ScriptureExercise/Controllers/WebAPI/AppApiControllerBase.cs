using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Common.Repositories;

namespace ScriptureExercise.Controllers.WebAPI
{
    [Route("[controller]/[action]")]
    [ApiController]
    public abstract class AppApiControllerBase : ControllerBase
    {
        //private readonly IMemoryCacheRepository repo;
        //public AppApiControllerBase(
        //    IMemoryCacheRepository repo
        //)
        //{
        //    this.repo = repo;
        //}

        //public IActionResult RemoveTest()
        //{
        //    repo.Remove("xx"); //什麼事都沒發生
        //    return Ok();
        //}

        //public IActionResult ExceptionTest()
        //{
        //    throw new Exception("QQ"); //狀態碼500
        //    return Ok();
        //}
    }
}
