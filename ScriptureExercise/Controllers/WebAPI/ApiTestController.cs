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
            var result = new ApiResult()
            {
                Status = Status.Success,
                Message = "qwe",
                Payload = new { prop1 }
            };
            return Ok(result);
        }
    }
}
