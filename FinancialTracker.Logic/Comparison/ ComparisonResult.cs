namespace FinanceApp.Services
{
    public class PeriodComparisonResult
    {
        public decimal CurrentTotal { get; set; }
        public decimal PreviousTotal { get; set; }
        public decimal Difference { get; set; }  // Current - Previous
        public string Label { get; set; } = "";
    }

    public class BudgetCheckResult
    {
        public decimal Spent { get; set; }
        public decimal Budget { get; set; }
        public bool IsOverBudget { get; set; }
        public string Label { get; set; } = "";
    }
}
