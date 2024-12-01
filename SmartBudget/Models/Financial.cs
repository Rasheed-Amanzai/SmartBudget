namespace SmartBudget.Models
{
    public class Financial
    {
        public int UserId { get; set; }
        public float Income { get; set; }
        public float Expenses { get; set; }
        public float PredictedSavings { get; set; }
    }
}