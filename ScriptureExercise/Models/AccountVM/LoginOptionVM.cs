using Common.Helpers;

namespace ScriptureExercise.Models.AccountVM
{
    public class LoginOptionVM
    {
        public LoginOptionVM(string displayName, string color, string scheme)
        {
            DisplayName = displayName;
            Color = color;
            Scheme = scheme;
            Icon = IconHelper.LoginIconDict[scheme];
        }

        public string DisplayName { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }

        public string Scheme { get; set; }
    }
}
