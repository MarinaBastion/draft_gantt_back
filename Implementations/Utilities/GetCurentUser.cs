using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gantt_backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Web;

namespace gantt_backend.Implementation.Utilities
{
    public class CurrentUserData: IDisposable
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public  CurrentUserData(IHttpContextAccessor httpContextAccessor)
        {
           
            _httpContextAccessor = httpContextAccessor;

        }
        public UserModel? GetCurrentUserInfo(){
            var Identity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity; 
            if (Identity != null)
            {
                var userClaims = Identity.Claims;
                return new UserModel{
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            else
            {
               return null;     
            }
        }

         public void Dispose() => Console.WriteLine($"");
    }
}