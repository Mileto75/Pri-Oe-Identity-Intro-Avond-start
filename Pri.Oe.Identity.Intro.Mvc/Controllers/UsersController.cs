using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Oe.Identity.Intro.Mvc.Services.Users;
using Pri.Oe.Identity.Intro.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pri.Oe.Identity.Intro.Mvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsersLoginViewModel usersLoginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(usersLoginViewModel);
            }
            await _userService.LoginAsync(usersLoginViewModel.UserName,
                usersLoginViewModel.Password);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult>Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Login");
        }

        [Authorize(Policy = "HasModule")]
        public IActionResult HasModule()
        {
            return View("Index");
        }
        [Authorize(Policy = "PriStudent")]
        public IActionResult Pri()
        {
            return View("Index");
        }
        [Authorize(Policy = "EmailAndModule")]
        public IActionResult EmailAndModule()
        {
            return View("Index");
        }
        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.Message = "You are not authorized!";
            return View();
        }



    }
}
