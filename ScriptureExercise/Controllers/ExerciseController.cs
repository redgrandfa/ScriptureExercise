using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptureExercise.Models.ExerciseVM;
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
            return View();
        }


        [HttpGet("CollectList")]
        public IActionResult CollectList()
        {
            return View();
        }

        //[HttpGet("{subject}")] //考慮： 論語(二)
        //public IActionResult Chapters(string subject)
        [HttpGet("{scriptureTitle}.{subjectId?}")] //此考科的 卷別們
        [AllowAnonymous]
        public IActionResult Chapters(string scriptureTitle, int subjectId = 1)
        {
            (string ScriptureCode, string ScriptureTitle, int SubjectId) vm = (
                ScriptureCode: ScriptureHelper.scriptureTrans[scriptureTitle], ///不存在會報錯
                ScriptureTitle: scriptureTitle,
                SubjectId: subjectId
            );

            return View(vm);
        }

        [HttpGet("{scriptureTitle}.{subjectId?}/卷{PaperId}")]
        public IActionResult Paper(string scriptureTitle,  int PaperId , int subjectId = 1)
        {
            (string ScriptureCode, string ScriptureTitle, int SubjectId, int PaperId) 
                vm = (
                ScriptureCode: ScriptureHelper.scriptureTrans[scriptureTitle], ///不存在會報錯
                ScriptureTitle: scriptureTitle,
                SubjectId : subjectId,
                PaperId: PaperId
            );

            return View(vm);
        }
        [HttpGet("Record/{CreateTimeId}")]
        public IActionResult Record(string CreateTimeId)
        {
            return View(model: CreateTimeId);
        }

        [HttpGet("RecordList")]
        public IActionResult RecordList()
        {
            return View();
        }
    }
}
