using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models;

namespace ScriptureExercise.Controllers.WebAPI
{
    public class ApiTestController : AppApiControllerBase
    {
        [HttpGet]
        public IActionResult TestGet()
        {
            throw new System.Exception("qwe");
        }
        [HttpPost]
        public IActionResult TestPost(string prop1 )
        {
            var result = new ApiResponseBody()
            {
                Status = ApiOperationStatus.Success,
                Message = "qwe",
                Payload = new { prop1 }
            };
            return Ok(result);
        }

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
