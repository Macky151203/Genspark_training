namespace Bank.Models;

public class BankAccount
{
    public int Id { get; set; }
    public string AccountHolderName { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; } = 0;
    public string AccountType { get; set; }
    public string AadharNumber { get; set; }
    public string MobileNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Transaction>? Transactions { get; set; }
}