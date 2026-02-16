using System;
using System.Collections.Generic;

namespace ClientLibrary.Models.Landing;

public record DashboardMetrics(int ActiveServices, int NewRequests, int InProgress, double Rating);
public record ServiceTransaction(DateTime Date, decimal Amount, string Category, string Status, string City);
public record ServiceGroup(string Category, List<ServiceSummary> Services);
public record ServiceSummary(string Slug, string Name, decimal Price, string Description);

public class ServiceFormModel
{
    public string? Slug { get; set; } // For Edit
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = "Limpieza y orden";
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
}

