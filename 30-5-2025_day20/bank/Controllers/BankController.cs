using Bank.Contexts;
using Bank.Interfaces;
using Bank.Models;
using Microsoft.AspNetCore.Mvc;
using Bank.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class BankController : ControllerBase
{
    private readonly IBankService _bankService;

    public BankController(IBankService bankService)
    {
        _bankService = bankService;
    }

    [HttpGet("allaccounts")]
    public async Task<IActionResult> GetAllAccounts()
    {
        var accounts = await _bankService.GetAllAccountsAsync();
        return Ok(accounts);
    }

    [HttpGet("balance/{accountNumber}")]
    public async Task<IActionResult> GetBalance(string accountNumber)
    {
        var balance = await _bankService.GetBalanceAsync(accountNumber);
        return Ok(balance);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] TransactionDto transaction)
    {
        var result = await _bankService.DepositAsync(transaction.BankAccountId, transaction.Amount);
        if (result)
            return Ok("Deposit successful");
        return BadRequest("Deposit failed");
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] TransactionDto transaction)
    {
        var result = await _bankService.WithdrawAsync(transaction.BankAccountId, transaction.Amount);
        if (result)
            return Ok("Withdrawal successful");
        return BadRequest("Withdrawal failed");
    }

    [HttpPost("create-account")]
    public async Task<IActionResult> CreateAccount([FromBody] AccountCreationDto account)
    {
        BankAccount newaccount= new BankAccount
        {
            Id=0,
            AccountHolderName = account.AccountHolderName,
            AccountNumber = Guid.NewGuid().ToString("N").Substring(0, 10), 
            AadharNumber = account.AadharNumber,
            MobileNumber = account.MobileNumber,
            AccountType = account.AccountType,
            CreatedAt = DateTime.UtcNow
        };
        var createdAccount = await _bankService.CreateAccountAsync(newaccount);
        return Created(" ",createdAccount);
    }
}
