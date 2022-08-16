

using System.ComponentModel.DataAnnotations;

namespace ScriptureExercise.Models.AccountVM
{
    public class UnTrackedAccountVM
    {
        [StringLength(12, MinimumLength = 6)]
        public string BindKey { get; set; }

        //public CreateMemberFormModel CreateMemberForm { get; set; }
        //public BindMemberFormModel BindMemberForm { get; set; }
    }

    //public class CreateMemberFormModel: BindMemberFormModel
    //{
    //    [Compare( nameof(CreateMemberFormModel.BindKey) )]
    //    public string BindKeyCheck { get; set; }
    //}

    //public class BindMemberFormModel
    //{
    //    [Required]
    //    [StringLength(12, MinimumLength =3) ]
    //    public string BindKey { get; set; }
    //}
}
