namespace SmartBudget.ViewModels
{
    public class UserPrivacyComplianceViewModel
    {
        public string UserName { get; set; }
        public bool NotificationEnabled { get; set; }
        public string DataVisibility { get; set; } // Private or Public
        public bool PrivacyAgreed { get; set; }
    }
}
