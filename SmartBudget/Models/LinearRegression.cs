namespace SmartBudget.Models
{
    public class LinearRegression
    {
        public float Predict(float income, float expenses)
        {
            // Simplified Linear Regression Formula: y = mx + b
            float m = 0.7f; // Example coefficient
            float b = 100;  // Example intercept
            return (m * (income - expenses)) + b;
        }
    }
}