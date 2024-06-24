using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DriveSafe.Presentation.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    
    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context, ITokenService tokenService, IUserRepository userRepository)
    {
        var allowAnonymous = await IsAllowAnonymousAsync(context);

        if (allowAnonymous)
        {
            await _next(context);
            return;
        }
        
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (token == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is missing");
            return;
        }
        
        
        var userId = await tokenService.ValidateToken(token);
        
        
        if (userId == null || userId == 0)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is corrupted");
            return;
        }
        
        
        var user = await userRepository.GetByIdAsync(userId.Value);
        context.Items["User"] = user;
        
        
        
        await _next(context);  
    }
    
   
