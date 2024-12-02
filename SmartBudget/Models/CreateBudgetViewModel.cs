namespace SmartBudget.Models
{
    public class CreateBudgetViewModel
    {
        // Budget Name
        public required string Name { get; set; }

        // Monthly Income Data
        public decimal EmploymentIncome { get; set; }
        public decimal MonthlyOtherIncome { get; set; }

        // Seasonal Income Data
        public decimal GovernmentSupport { get; set; }
        public decimal Awards { get; set; }
        public decimal FamilySupport { get; set; }
        public decimal SeasonalOtherIncome { get; set; }

        // Monthly Expense Data
        public decimal Rent { get; set; }
        public decimal Food { get; set; }
        public decimal Utilities { get; set; }
        public decimal Transportation { get; set; }
        public decimal Entertainment { get; set; }
        public decimal MonthlyOtherExpense { get; set; }

        // Seasonal Expense Data
        public decimal Tuition { get; set; }
        public decimal AcademicMaterials { get; set; }
        public decimal Travel { get; set; }
        public decimal SeasonalOtherExpense { get; set; }
    }
}
