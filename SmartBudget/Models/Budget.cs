namespace SmartBudget.Models
{
    public class Budget
    {
        // Budget Name
        public required string Name { get; set; }

        // Income Data
        public Dictionary<string, decimal> MonthlyIncome { get; set; }
        public Dictionary<string, decimal> SeasonalIncome { get; set; }

        // Expenses Data
        public Dictionary<string, decimal> MonthlyExpenses { get; set; }
        public Dictionary<string, decimal> SeasonalExpenses { get; set; }

        // Savings Data
        public decimal MonthlySavings { get; private set; }

        public Budget()
        {
            // Calculate and initialize monthly savings
            CalculateMonthlySavings();
        }

        // Method for calculating the monthly savings, given the monthly income and expenses
        public void CalculateMonthlySavings()
        {
            decimal totalIncome = 0;
            decimal totalExpenses = 0;

            foreach (var income in MonthlyIncome.Values)
            {
                totalIncome += income;
            }

            foreach (var expense in MonthlyExpenses.Values)
            {
                totalExpenses += expense;
            }

            MonthlySavings = totalIncome - totalExpenses;
        }
    }
}