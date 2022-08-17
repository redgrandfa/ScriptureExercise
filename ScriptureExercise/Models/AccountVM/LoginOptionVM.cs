namespace ScriptureExercise.Models.AccountVM
{
    public class LoginOptionVM
    {
        public LoginOptionVM(string displayName, string color, string icon , string scheme)
        {
            DisplayName = displayName;
            Color = color;
            Icon = icon;
            Scheme = scheme;
        }

        public string DisplayName { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }

        public string Scheme { get; set; }
    }
}
