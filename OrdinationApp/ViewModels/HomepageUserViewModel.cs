namespace OrdinationApp.ViewModels
{
    public class HomepageUserViewModel
    {
        public string UserName { get; set; }
        public string Role { get;set; }
        public HomepageUserViewModel(string username, string role)
        {
            this.UserName = username;
            this.Role = role;
        }
    }
}
