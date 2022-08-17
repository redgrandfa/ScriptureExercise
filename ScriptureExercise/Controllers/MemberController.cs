using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models.MemberVM;
using ScriptureExercise.Services;
using System.Collections.Generic;

namespace ScriptureExercise.Controllers
{
    [Authorize(Roles ="Member")]
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;
        public MemberController(
            IMemberService memberService
            )
        {
            this.memberService = memberService;
        }

        public IActionResult Settings()
        {

            var vm = new MemberEditViewMdel
            {
                LoginThroughIcon = "fa-brands fa-facebook",
                NumberOfQuestionsCorrect = 11,
                NumberOfQuestionsDone = 22,
                Editable = new MemberUpdateRequestModel
                {
                    Name = "瑋軒",
                    AutoDownload = true,
                    ScriptueShowList = new List<int>
                    {1,
                    },
                }
            };

            return View(vm);
        }

        public IActionResult ExerciseHistory()
        {
            return View();
        }
    }
}
