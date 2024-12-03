using System.ComponentModel.DataAnnotations;

namespace SmartBudget.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public string UserId { get; set; } // Foreign Key to IdentityUser

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
