using linksnews2API.Models;
using linksnews2API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace linksnews2API.Controllers;
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    public readonly IAccountService _accountService;
    public readonly IUserService _userService;
    public AccountController(IAccountService accountService, IUserService userService)
    {
        _accountService = accountService;
        _userService = userService;
    }


    private async Task<bool> checkUser()
    {
        string? value = null;
        var headers = HttpContext.Request.Headers;

        foreach (var h in headers)
        {
            if (h.Key == "login") {
                value = h.Value;
                break;
            }
        }

        if (value == null) 
        {
            throw new Exception("No login provided");
        }

        dynamic login = JsonConvert.DeserializeObject(value);
        string name = login.name;
        string password = login.password;

        bool userFound = await _userService.CheckLogin(name, password);

        return userFound;
    }


    [HttpGet]
    public async Task<IActionResult> GetCurrent()
    {

        if (!await checkUser())
        {
            throw new Exception("User not found");
        }

        string userName = _userService.CurrentUser;
        if (userName == null) 
        {
            throw new Exception("No current user");
        }
        Account result = await _accountService.GetByName(userName);
        if (result == null)
        {
            throw new Exception("Account found is null");
        }

        return Ok(result);
    }

    // [HttpGet("account/{id}")]
    // public async Task<IActionResult> GetById(string id)
    // {
    //     if (await checkUser() == null)
    //     {
    //         throw new Exception("User not found");
    //     }
    //     var result = await _accountService.GetById(id);
    //     return Ok(result);
    // }

    

    // [HttpPost]
    // public async Task<IActionResult> Post(Account newAccount)
    // {
    //     newAccount.Id = Guid.NewGuid().ToString();
    //     var result = await _accountService.Add(newAccount);
    //     return Ok(result);
    // }

    [HttpPut]
    public async Task<IActionResult> Put(Account accountToUpdate)
    {
        if (!await checkUser())
        {
            throw new Exception("User not found");
        }
        var result = await _accountService.Update(accountToUpdate);
        return Ok(result);
    }

    // [HttpDelete]
    // public async Task<IActionResult> Delete(string id)
    // {
    //     await _accountService.Delete(id);
    //     return Ok();
    // }
}
