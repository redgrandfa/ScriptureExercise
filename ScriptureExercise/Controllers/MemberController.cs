using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScriptureExercise.Controllers
{
    [Authorize(Roles ="Member")]
    public class MemberController : Controller
    {

        public IActionResult Settings()
        {
            //姓名
            //密鑰
            //偏好
                //顯示在經典選單 中的一部分 => filter
            return View();
            //總做題數
            //總答對數
        }

        public IActionResult ExerciseHistory()
        {
            return View();
        }
    }
}
