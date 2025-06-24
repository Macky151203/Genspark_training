
interface ISalary
{
    double CalculateSalary();
}

interface CodeReview {
    void ReviewCode();
}
interface ILeaveApproval
{
    void ApproveLeave();
}
class Manager : CodeReview, ILeaveApproval
{
    public void ReviewCode()
    {
        Console.WriteLine("Manager is reviewing code");
    }
    public void ApproveLeave()
    {
        Console.WriteLine("Manager is approving leave");
    }
}

class HR : ILeaveApproval
{
    public void ApproveLeave()
    {
        Console.WriteLine("HR is approving leave");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // ISP
        Manager manager = new Manager();
        manager.ReviewCode();
        manager.ApproveLeave();

        // HR hr = new HR();
        // hr.ApproveLeave();
    }
}
