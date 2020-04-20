using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Repository;

namespace API.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository _userRepository)
        {
            if (context.Request.Headers.TryGetValue("ApiKey", out var _id))
            {
                if (Guid.TryParse(_id, out var Id))
                {
                    var user = await _userRepository.GetByIdAsync(Id);
                
                    if (user != null)
                    {
                        var role= (Business.Models.User.Roles) user.Role;    
                    
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Role, role.ToString())
                        };
                        
                        var cli = new ClaimsIdentity(claims, "ApiKey");
                        cli.AddClaims(claims);

                        context.User.AddIdentity(cli);
                    }
                }
            }
            
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
        
    }
}