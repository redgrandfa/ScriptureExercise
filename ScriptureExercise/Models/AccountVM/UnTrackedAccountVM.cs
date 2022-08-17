using System.ComponentModel.DataAnnotations;

namespace ScriptureExercise.Models.AccountVM
{
    public class CreateMemberFormModel
    {
        [StringLength(12, MinimumLength = 3)]
        public string BindKey { get; set; }
    }
}
