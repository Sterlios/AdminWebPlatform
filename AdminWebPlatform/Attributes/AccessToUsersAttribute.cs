using AdminWebPlatform.Contexts;
using AdminWebPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace AdminWebPlatform.Attributes
{
    public class AccessToUsersAttribute : Attribute, IAuthorizationFilter
    {
        private readonly AccessLevel _requiredAccess;

        public AccessToUsersAttribute(AccessLevel requiredAccess) =>
            _requiredAccess = requiredAccess;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            var userId = int.Parse(user.FindFirst("UserId")?.Value ?? "0");
            var dbContext = context.HttpContext.RequestServices.GetService<ApplicationDbContext>();
            var userFromDb = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == userId);

            if (userFromDb == null || (userFromDb.Role.UserAccessLevel & _requiredAccess) != _requiredAccess)
                context.Result = new ForbidResult();
        }
    }
}
