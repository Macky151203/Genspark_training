public class StudentReport
{
    public string? StudentName { get; set; }
    public string? ReportContent { get; private set; }

    public void GenerateReport()
    {
        ReportContent = $"Report for {StudentName}";
        Console.WriteLine("Report generated successfully.");
    }
}

public class ReportSaver
{
    public void SaveToFile(StudentReport report)
    {
        Console.WriteLine($"Saving report to file: {report.ReportContent}");
        
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Single Responsibility Principle
        StudentReport report = new StudentReport { StudentName = "abc" };
        report.GenerateReport();

        ReportSaver saver = new ReportSaver();
        saver.SaveToFile(report);

        
    }
}