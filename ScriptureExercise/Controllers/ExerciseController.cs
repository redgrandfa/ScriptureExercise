using Microsoft.AspNetCore.Mvc;

namespace ScriptureExercise.Controllers
{
    public class ExerciseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("{subject}/{page}")]
        public IActionResult Index(string subject , int page)
        {
            return View();
        }
    }
}
