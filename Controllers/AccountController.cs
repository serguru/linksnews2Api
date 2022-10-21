using linksnews2API.Models;
using linksnews2API.Services;
using Microsoft.AspNetCore.Mvc;

namespace linksnews2API.Controllers;
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    public readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _accountService.GetAll();
        return Ok(result);
    }

    [HttpGet("account/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _accountService.GetById(id);
        return Ok(result);
    }

    [HttpGet("default")]
    public async Task<IActionResult> GetDefaultAccount()
    {
        var result = await _accountService.GetById();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Account newAccount)
    {
        newAccount.Id = Guid.NewGuid().ToString();
        var result = await _accountService.Add(newAccount);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put(Account accountToUpdate)
    {
        var result = await _accountService.Update(accountToUpdate);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        await _accountService.Delete(id);
        return Ok();
    }
}
