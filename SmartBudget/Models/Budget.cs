namespace SmartBudget.Models
{
    public class Budget
    {
        // Budget Name
        public string Name { get; set; }

        // Income Data
        public Dictionary<string, decimal> MonthlyIncome { get; set; }
        public Dictionary<string, decimal> SeasonalIncome { get; set; }
        public decimal TotalMonthlyIncome { get; private set; }

        // Expenses Data
        public Dictionary<string, decimal> MonthlyExpenses { get; set; }
        public Dictionary<string, decimal> SeasonalExpenses { get; set; }
        public decimal TotalMonthlyExpense { get; private set; }

        // Savings Data
        public decimal MonthlySavings { get; private set; }

        public Budget(CreateBudgetViewModel model)
        {
            // Initialize budget name
            Name = model.Name;

            // Initialize dictionaries
            MonthlyIncome = new Dictionary<string, decimal>
            {
                { "Employment", model.EmploymentIncome },
                { "Other", model.MonthlyOtherIncome }
            };
            SeasonalIncome = new Dictionary<string, decimal>
            {
                { "Government", model.GovernmentSupport },
                { "Awards", model.Awards },
                { "Family", model.FamilySupport },
                { "Other", model.SeasonalOtherIncome }
            };
            MonthlyExpenses = new Dictionary<string, decimal>
            {
                { "Rent", model.Rent },
                { "Food", model.Food },
                { "Utilities", model.Utilities },
                { "Transportation", model.Transportation },
                { "Entertainment", model.Entertainment },
                { "Other", model.MonthlyOtherExpense }
            };
            SeasonalExpenses = new Dictionary<string, decimal>
            {
                { "Tuition", model.Tuition },
                { "Academic Materials", model.AcademicMaterials },
                { "Travel", model.Travel },
                { "Other", model.SeasonalOtherExpense }
            };

            // Calculate and initialize the total monthly income and expenses
            GetTotalMonthlyIncome();
            GetTotalMonthlyExpense();

            // Calculate the monthly savings
            MonthlySavings = TotalMonthlyIncome - TotalMonthlyExpense;
        }

        // Method for calculating the total monthly income
        private void GetTotalMonthlyIncome()
        {
            TotalMonthlyIncome = 0;

            foreach (var income in MonthlyIncome.Values)
            {
                TotalMonthlyIncome += income;
            }
        }

        // Method for calculating the total monthly expenses
        private void GetTotalMonthlyExpense()
        {
            TotalMonthlyExpense = 0;

            foreach (var expense in MonthlyExpenses.Values)
            {
                TotalMonthlyExpense += expense;
            }
        }
    }
}