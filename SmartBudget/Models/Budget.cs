namespace SmartBudget.Models
{
    public class Budget
    {
        // Budget Name
        public required string Name { get; set; }

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

        public Budget()
        {
            // Calculate and initialize the total monthly income and expenses
            GetTotalMonthlyIncome();
            GetTotalMonthlyExpense();

            // Calculate the monthly savings
            MonthlySavings = TotalMonthlyIncome - TotalMonthlyExpense;
        }

        // Method for calculating the total monthly income
        public void GetTotalMonthlyIncome()
        {
            foreach (var income in MonthlyIncome.Values)
            {
                TotalMonthlyIncome += income;
            }
        }

        // Method for calculating the total monthly expenses
        public void GetTotalMonthlyExpense()
        {
            foreach (var expense in MonthlyExpenses.Values)
            {
                TotalMonthlyExpense += expense;
            }
        }
    }
}