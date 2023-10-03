using FoolCardGame.API.Helpers;
using Microsoft.AspNetCore.Identity;

namespace FoolCardGame.Identity.Services;

public class AuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityUser> Authenticate(string email, string password)
    {
        if (string.IsNullOrEmpty(email))
            throw new AppException("Email is incorrect.");
        
        if (string.IsNullOrEmpty(password))
            throw new AppException("Password is incorrect.");
        
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new AppException("User not found.");
        
        var result = await _signInManager.PasswordSignInAsync(user.UserName!, password, false, false);

        if (result.Succeeded == false)
        {
            throw new AppException("Password is incorrect.");
        }

        return user;
    }

    public async Task<IdentityUser> Create(string username, string email, string password)
    {
        if (string.IsNullOrEmpty(email))
            throw new AppException("Email is incorrect.");
        
        if (string.IsNullOrEmpty(password))
            throw new AppException("Password is incorrect.");
        
        var findingUser = await _userManager.FindByEmailAsync(email);

        if (findingUser != null)
            throw new AppException("User with this email already exists.");
        
        var user = new IdentityUser
        {
            UserName = username,
            Email = email
        };
        
        var result = await _userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return user;
        }
        
        throw new AppException("User with this email already exists.");
    }
}