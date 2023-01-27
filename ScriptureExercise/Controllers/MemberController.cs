using Common.DBModels;
using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models.MemberVM;
using ScriptureExercise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
            var memberId = int.Parse(User.Identity.Name);
            Member.Value_T memberValue = memberService.GetMember_ById(memberId).Value;
            if(memberValue == null)
            {
                return Content("查無當前當入的會員身分，請重新登入");
            }

            var counter = memberService.GetEntityCounter();

            var vm = new MemberCenterVM
            {
                //LoginThroughIcon = IconHelper.LoginIconDict[
                //    User.Claims.FirstOrDefault(c => c.Type == Define.LOGIN_THROUGH_CLAIM_TYPE)?.Value
                //],
                ChoicesQuestion_Correct = memberValue.ChoicesQuestion_Correct,
                EssayQuestion_Correct = memberValue.EssayQuestion_Correct,
                BlankFillQuestion_Correct = memberValue.BlankFillQuestion_Correct,
                ChoicesQuestion_Done = memberValue.ChoicesQuestion_Done,
                EssayQuestion_Done = memberValue.EssayQuestion_Done,
                BlankFillQuestion_Done = memberValue.BlankFillQuestion_Done,
                
                ExamDate = new DateTime(
                    counter.ExamDate_Y,
                    counter.ExamDate_M,
                    counter.ExamDate_D,
                    20,0,0),


                Editable = new MemberEditVM
                {
                    Name = memberValue.Name,
                    Account = memberValue.AccountPK_List[0].AccountId_FromProvider,
                }
            };

            return View(vm);
        }
    }
}
