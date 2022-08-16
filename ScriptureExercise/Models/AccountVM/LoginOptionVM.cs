namespace ScriptureExercise.Models.AccountVM
{
    public class LoginOptionVM
    {
        public LoginOptionVM(string displayName, string scheme)
        {
            DisplayName = displayName;
            Scheme = scheme;
        }

        public string Scheme { get; set; }

        public string DisplayName { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
    }
}
