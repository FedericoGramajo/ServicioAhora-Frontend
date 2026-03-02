namespace ClientLibrary.Models.Dashboard;

public class AdminReportsResponse
{
    public List<UserGrowthMetric> UserGrowth { get; set; } = new();
    public List<CategoryBalanceMetric> CategoryBalance { get; set; } = new();
    public decimal TotalRevenue { get; set; }
    public List<QualityRankingMetric> QualityRanking { get; set; } = new();
    public List<AuditSummaryMetric> AuditSummary { get; set; } = new();
}

public class UserGrowthMetric
{
    public string Month { get; set; } = string.Empty;
    public int CustomerCount { get; set; }
    public int ProfessionalCount { get; set; }
}

public class CategoryBalanceMetric
{
    public string CategoryName { get; set; } = string.Empty;
    public int RequestCount { get; set; }
    public int ProfessionalCount { get; set; }
}

public class QualityRankingMetric
{
    public string ProfessionalId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public double AverageRating { get; set; }
}

public class AuditSummaryMetric
{
    public string Action { get; set; } = string.Empty;
    public int Count { get; set; }
}
