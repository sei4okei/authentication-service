using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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

                var token = _tokenService.CreateAccessToken(user);

                return new ResponseModel
                {
                    Action = "Login",
                    Code = "200",
                    Token = token,
                    User = user.Email
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

                var user = new IdentityUser
                {
                    UserName = model.Login,
                    Email = model.Login
                };
                var isCreated = await _userManager.CreateAsync(user, model.Password);

                if (!isCreated.Succeeded)
                {
                    return new ResponseModel
                    {
                        Action = "Registration",
                        Code = "400",
                        Error = "Incorrect password"
                    };
                }

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
