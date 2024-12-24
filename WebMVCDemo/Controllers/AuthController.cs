﻿using Application.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.IService;

namespace WebMVCDemo.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _userService.Authenticate(email, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role!.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            if (user.Role!.Name == "Admin")
            {
                return RedirectToAction("AdminIndex", "Home");
            }
            else if (user.Role!.Name == "User")
            {
                return RedirectToAction("UserIndex", "Home");
            }

            return RedirectToAction("UserIndex", "Home");
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userDto);
            }

            var userRole = await _roleService.GetRoleByNameAsync("User");
            if (userRole == null)
            {
                ModelState.AddModelError("", "User role does not exist. Please contact the administrator.");
                return View(userDto);
            }

            userDto.RoleId = userRole.Id;

            var success = await _userService.RegisterUserAsync(userDto);
            if (!success)
            {
                ModelState.AddModelError("", "Registration failed. Email may already be in use.");
                return View(userDto);
            }

            TempData["SuccessMessage"] = "Account created successfully! Please log in.";
            return RedirectToAction("Login");
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
