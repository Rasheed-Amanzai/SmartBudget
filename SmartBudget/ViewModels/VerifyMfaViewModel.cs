namespace SmartBudget.ViewModels
{
    public class VerifyMfaViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool RememberClient { get; set; }
        public string ReturnUrl { get; set; }
    }
}
