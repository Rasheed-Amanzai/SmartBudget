using System.ComponentModel.DataAnnotations;

namespace SmartBudget.Models
{
    public class BudgetReport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        [Required]
        public string UserId { get; set; } // Foreign Key to IdentityUser

        public DateTime CreatedAt { get; set; }
    }
}
