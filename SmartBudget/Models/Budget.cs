namespace SmartBudget.Models
{
    public class Budget
    {
        public string Username { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Savings => Income - Expenses;

        public static List<Budget> Budgets = new List<Budget>();

        public static void CreateBudget(string username, decimal income, decimal expenses)
        {
            Budgets.Add(new Budget { Username = username, Income = income, Expenses = expenses });
        }

        public static Budget GetBudget(string username)
        {
            return Budgets.FirstOrDefault(b => b.Username == username);
        }
    }
}