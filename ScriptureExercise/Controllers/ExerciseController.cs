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

        [HttpGet("Choices")]
        public IActionResult List()
        {        
            var member = memberService.GetCurrentMember();
            return View(member.Value.ScriptureShowList);
        }

        [HttpGet("{Scripture}_{subjectId}/卷別{PaperId}")]
        public IActionResult Paper(string Scripture, int subjectId, int PaperId)
        {
            var vm = new ExcerciseListVM();

            //try
            //{
            //}
            //catch(Exception ex)
            //{
            //    return Content("錯誤的經典名稱。"+ ex.Message );
            //};
            vm.ScriptureName = Scripture;
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
