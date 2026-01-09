namespace BlazorWasm.Pages.AdminDashboard;

public record AdminMetric(string Label, string Value, string Subtitle);

public record ReportBar(string Label, int Count)
{
    public double Percentage(int max)
    {
        if (max <= 0)
        {
            return 0;
        }

        return Math.Round((double)Count / max * 100, 1);
    }
}
