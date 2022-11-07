using linksnews2API.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace FiltersSample.Filters;

public class GlobalActionFilter : IActionFilter
{

    public readonly IUserService _userService;
    
    public GlobalActionFilter(IUserService userService)
    {
        _userService = userService;
    }

    public async void OnActionExecuting(ActionExecutingContext context)
    {
        // Console.WriteLine(
        //     $"- {nameof(GlobalSampleActionFilter)}.{nameof(OnActionExecuting)}");


        string? value = null;
        var headers = context.HttpContext.Request.Headers;

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
//        IUserService service = context.HttpContext.RequestServices.GetService<IUserService>();

        bool userFound = await _userService.CheckLogin(name, password);

        if (!userFound) 
        {
            throw new Exception("User not found");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Console.WriteLine(
        //     $"- {nameof(GlobalSampleActionFilter)}.{nameof(OnActionExecuted)}");
    }
}
