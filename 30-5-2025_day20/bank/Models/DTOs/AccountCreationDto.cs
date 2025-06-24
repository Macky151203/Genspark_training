namespace Bank.Models.DTOs;
public class AccountCreationDto
{
  
    public string AccountHolderName { get; set; }
    public string AadharNumber { get; set; }
    public string MobileNumber { get; set; }
    public string AccountType { get; set; }

    public AccountCreationDto(string accountHolderName, string aadharNumber, string mobileNumber, string accountType)
    {
        AccountHolderName = accountHolderName;
        AadharNumber = aadharNumber;
        MobileNumber = mobileNumber;
        AccountType = accountType;
    }
}