namespace ClientLibrary.Models.Dashboard;

public class CustomerReportsResponse
{
    public List<ExpenseHistoryMetric> ExpenseHistory { get; set; } = new();
    public List<ServiceFrequencyMetric> ServiceFrequency { get; set; } = new();
    public List<StatusSummaryMetric> StatusSummary { get; set; } = new();
    public List<RatingGivenMetric> RatingsGiven { get; set; } = new();
}

public class ExpenseHistoryMetric
{
    public string Month { get; set; } = string.Empty;
    public decimal TotalSpent { get; set; }
}

public class ServiceFrequencyMetric
{
    public string CategoryName { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class StatusSummaryMetric
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class RatingGivenMetric
{
    public string CustomerName { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
