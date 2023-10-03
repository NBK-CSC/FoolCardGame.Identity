using FoolCardGame.API.Helpers;
using FoolCardGame.Identity.Models;
using FoolCardGame.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoolCardGame.Identity.Controllers;

public class AuthController : Controller
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestModel requestModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.ValidationState);
        
        return await TryPerformAction(
            async () => await _authService.Authenticate(requestModel.Email, requestModel.Password));
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestModel requestModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ValidationState);
        }

        return await TryPerformAction(
            async () => await _authService.Create(requestModel.UserName, requestModel.Email, requestModel.Password));
    }

    private async Task<IActionResult> TryPerformAction(Func<Task> action)
    {
        try
        {
            await action.Invoke();
            return Ok();
        }
        catch (AppException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /*
    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await _signInManager.SignOutAsync();
        var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }*/
}