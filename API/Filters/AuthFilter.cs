using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class AuthFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authAttribute = (AuthorizeAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(e => e.GetType() == typeof(AuthorizeAttribute));

            try
            {
                if (authAttribute == null) return; //No auth required for method
                if (!context.HttpContext.User.Identities.Any()) return; //User has no identities
                
                //If the user is Valid
                var roles = authAttribute.Roles.Split(',');
                if (roles.Any(role => context.HttpContext.User.IsInRole(role)))
                    return;
                
                throw new UnauthorizedAccessException();
            }
            catch
            {
                context.HttpContext.Response.StatusCode = 500;
                context.Result = new JsonResult("Failure");
            }
        }
    }
}