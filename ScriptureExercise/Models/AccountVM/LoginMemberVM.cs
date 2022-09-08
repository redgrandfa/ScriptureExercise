using System.ComponentModel.DataAnnotations;


namespace ScriptureExercise.Models.AccountVM
{
    public class CreateMemberPostModel
    {
        [StringLength(12, MinimumLength = 3, ErrorMessage = "長度須為3~12字")]
        [RegularExpression(@"^\w+$", ErrorMessage = "須為英數混合")]
        public string Account { get; set; }

        [StringLength(12, MinimumLength = 3, ErrorMessage = "長度須為3~12字")]
        [RegularExpression(@"^\w+$", ErrorMessage = "須為英數混合")]
        public string Password { get; set; }
    }
}
