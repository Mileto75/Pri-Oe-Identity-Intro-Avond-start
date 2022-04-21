using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Pri.Oe.Identity.Intro.Mvc.ApplicationClaimTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pri.Oe.Identity.Intro.Mvc.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //logs the user in
        public async Task LoginAsync(string userName, string password)
        {
            //check hardcoded credentials
            //do not do this! Ever! for testing only
            if(userName == "someone" && password == "password")
            {
                //create a list of claims(normally froma database)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"Jimi Hendrix"),
                    new Claim(AppClaimTypes.Module,"Pri"),
                    new Claim(AppClaimTypes.Module,"Wsi"),
                    new Claim(AppClaimTypes.Module,"Cia"),
                    new Claim(ClaimTypes.Email,"Jimi.Hendrix@student.howest.be"),
                    new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.DateOfBirth,new DateTime(1975,12,12).ToShortDateString()),
                };
                //create identity for claims
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                //create the claimsPrincipal (principal user)
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //sign in the user
                await _httpContextAccessor
                    .HttpContext
                    .SignInAsync("CookieAuth",claimsPrincipal);
            }
            //user without module
            if (userName == "johnny" && password == "test")
            {
                //create a list of claims(normally froma database)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"Johnny De Beer"),
                    new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.DateOfBirth,new DateTime(1975,12,12).ToShortDateString()),
                };
                //create identity for claims
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                //create the claimsPrincipal (principal user)
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //sign in the user
                await _httpContextAccessor
                    .HttpContext
                    .SignInAsync("CookieAuth", claimsPrincipal);
            }
            //user without module
            if (userName == "student" && password == "test")
            {
                //create a list of claims(normally froma database)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"Mike Tyson"),
                    new Claim(AppClaimTypes.Module,"Wsi"),
                    new Claim(ClaimTypes.Email,"mike.deBeer@student.howest.be"),
                    new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.DateOfBirth,new DateTime(1975,12,12).ToShortDateString()),
                };
                //create identity for claims
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                //create the claimsPrincipal (principal user)
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //sign in the user
                await _httpContextAccessor
                    .HttpContext
                    .SignInAsync("CookieAuth", claimsPrincipal);
            }
        }
        

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
