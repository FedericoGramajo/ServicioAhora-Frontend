namespace ClientLibrary.Models.Dashboard;

public class ProfessionalReportsResponse
{
    public List<MonthlyIncomeMetric> MonthlyIncome { get; set; } = new();
    public double ScheduleEfficiency { get; set; }
    public List<CategoryPerformanceMetric> CategoryPerformance { get; set; } = new();
    public List<CustomerFeedbackMetric> CustomerFeedback { get; set; } = new();
    public double ConversionRate { get; set; }
}

public class MonthlyIncomeMetric
{
    public string Month { get; set; } = string.Empty;
    public decimal Income { get; set; }
}

public class CategoryPerformanceMetric
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal Earnings { get; set; }
}

public class CustomerFeedbackMetric
{
    public string CustomerName { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
