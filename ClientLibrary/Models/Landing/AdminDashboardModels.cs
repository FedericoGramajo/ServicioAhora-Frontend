using System;

namespace ClientLibrary.Models.Landing;

public record AdminMetric(string Label, string Value, string Subtitle);

public record ReportBar(string Label, int Count)
{
    public double Percentage(int max)
    {
        if (max <= 0) return 0;
        return Math.Round((double)Count / max * 100, 1);
    }
}

public record GrowthMetric(string Month, int Customers, int Professionals);

public record BalanceMetric(string CategoryName, int Orders, int Professionals);

public record FinanceMetric(decimal TotalRevenue, int CompletedOrders, decimal AverageOrderValue);

public record QualityMetric(string ProfessionalName, double Rating, int CanceledOrders, string Status);

public class AdminReportsResponse
{
    public List<GrowthMetric> Growth { get; set; } = new();
    public List<BalanceMetric> Balance { get; set; } = new();
    public FinanceMetric Finance { get; set; } = new(0, 0, 0);
    public List<QualityMetric> Quality { get; set; } = new();
}
