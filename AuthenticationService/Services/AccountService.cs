using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Models.DTOs;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Packaging.Signing;

namespace AuthenticationService.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly ServiceContext _context;

        public AccountService(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, ServiceContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<ResponseModel> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Login);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Action = "Login",
                        Code = "400",
                        Error = "Login or password entered incorrectly"
                    };
                }

                var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!isPasswordCorrect)
                {
                    return new ResponseModel
                    {
                        Action = "Login",
                        Code = "400",
                        Error = "Login or password entered incorrectly"
                    };
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (!result.Succeeded)
                {
                    return new ResponseModel
                    {
                        Action = "Login",
                        Code = "500",
                        Error = "Internal server error"
                    };
                }

                return new ResponseModel
                {
                    Action = "Login",
                    Code = "200"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Action = "Login",
                    Code = "500",
                    Error = "Internal server error"
                };
            }
        }

        public async Task<ResponseModel> Register(RegisterModel model)
        {
            try
            {
                var exist = await _userManager.FindByEmailAsync(model.Login);

                if (exist != null)
                {
                    return new ResponseModel
                    {
                        Action = "Registration",
                        Code = "400",
                        Error = "User with that login exist"
                    };
                }

                var user = new UserModel
                {
                    UserName = model.Login,
                    Email = model.Login
                };
                var isCreated = await _userManager.CreateAsync(user, model.Password);

                if (isCreated.Succeeded) await _userManager.AddToRoleAsync(user, UserRoles.User);

                return new ResponseModel
                {
                    Action = "Registration",
                    Code = "200"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Action = "Registration",
                    Code = "500",
                    Error = "Internal server error"
                };
            }
        }
    }
}
