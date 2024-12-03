using System.ComponentModel.DataAnnotations;

namespace SmartBudget.Models
{
    public class UserPreferences
    {
        [Key]
        public int Id { get; set; }

        public bool NotificationEnabled { get; set; }

        public string DataVisibility { get; set; } // Can be 'Private' or 'Public'

        public bool PrivacyAgreed { get; set; }

        public string UserId { get; set; } // Foreign Key to IdentityUser
    }
}
