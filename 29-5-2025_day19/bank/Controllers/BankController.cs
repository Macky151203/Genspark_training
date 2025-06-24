using Bank.Contexts;
using Bank.Interfaces;
using Bank.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BankController : ControllerBase
{
    private readonly IBankService _bankService;

    public BankController(IBankService bankService)
    {
        _bankService = bankService;
    }

    [HttpGet("balance/{accountNumber}")]
    public async Task<IActionResult> GetBalance(string accountNumber)
    {
        var balance = await _bankService.GetBalanceAsync(accountNumber);
        return Ok(balance);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] Transaction transaction)
    {
        var result = await _bankService.DepositAsync(transaction.BankAccountId, transaction.Amount);
        if (result)
            return Ok("Deposit successful");
        return BadRequest("Deposit failed");
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] Transaction transaction)
    {
        var result = await _bankService.WithdrawAsync(transaction.BankAccountId, transaction.Amount);
        if (result)
            return Ok("Withdrawal successful");
        return BadRequest("Withdrawal failed");
    }

    [HttpPost("create-account")]
    public async Task<IActionResult> CreateAccount([FromBody] BankAccount account)
    {
        var createdAccount = await _bankService.CreateAccountAsync(account);
        return CreatedAtAction(nameof(GetBalance), new { accountId = createdAccount.Id }, createdAccount);
    }
}
