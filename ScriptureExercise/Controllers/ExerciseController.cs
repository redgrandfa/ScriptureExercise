using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models.ExcerciseVM;
using ScriptureExercise.Services;
using System;
using System.Collections.Generic;

namespace ScriptureExercise.Controllers
{
    [Route("~/[controller]")]
    [Authorize(Roles = "Member")]
    public class ExerciseController : Controller
    {
        private readonly IMemberService memberService;
        //private readonly IExerciseService exerciseService;

        public ExerciseController(
            IMemberService memberService
            //, IExerciseService exerciseService
            )
        {
            this.memberService = memberService;
            //this.exerciseService = exerciseService;
        }

        [HttpGet("Subjects")]
        [AllowAnonymous]
        public IActionResult Subjects()
        {
            //var member = memberService.GetCurrentMember();
            //member.Value.ScriptureShowList;

            return View(); //改成收藏List=> 最後我得手改每個會員的收藏?...
        }

        //[HttpGet("{subject}/Chapters")] //此考科的 章節
        //public IActionResult Chapters(string subject) { 
        //    var scripture = subject;
        //    var paper = 1;
        //        if (subject.Contains("("))
        //        {
        //            var temp = subject.Split("(");
        //    scripture = temp[0];
        //            paper = temp[1][0];
        //        }

        //        return View(model:subject);
        //}

        [HttpGet("{scriptureTitle}_{subjectId}")] //此考科的 卷別
        [AllowAnonymous]
        public IActionResult Chapters(string scriptureTitle, int subjectId = 1)
        {
            ViewData["ScriptureTitle"] = scriptureTitle;
            ViewData["SubjectId"] = subjectId;

            return View();
        }

        [HttpGet("{scripture}_{subjectId}/卷{PaperId}")]
        public IActionResult Paper(string scripture, int subjectId, int PaperId)
        {
            var vm = new ExcerciseListVM();

            //try
            //{
            //}
            //catch(Exception ex)
            //{
            //    return Content("錯誤的經典名稱。"+ ex.Message );
            //};
            vm.ScriptureName = scripture;
            vm.SubjectId = subjectId;
            vm.PageId = PaperId;


            //在前端檢查 考卷是否真的存在? 只是要防止 亂打網址?
            //前端用JS語法引入JS 引不了可報錯?
            //這裡用列舉?? 百位 十位 個位數 ...10+26 = 36進位?? xy_zz 時間? 0~60
            return View(vm);
        }
        
        [HttpGet("RecordList")]
        public IActionResult RecordList()
        {
            return View();
        }

        [HttpGet("Record/{CreateTimeId}")]
        public IActionResult Record( string CreateTimeId )
        {
            return View(model:CreateTimeId);
        }
    }
}
