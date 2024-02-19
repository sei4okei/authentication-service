using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Repository;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuthenticationService.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAccountRepository _accountRepository;

        public AccountService(UserManager<User> userManager, ITokenService tokenService, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _accountRepository = accountRepository;
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

                if (token == null) return new ResponseModel { Action = "Login", Code = "500", Error = "Can't create access token" };

                var refreshToken = _tokenService.CreateRefreshToken(user);

                if (refreshToken == null) return new ResponseModel { Action = "Login", Code = "500", Error = "Can't create refresh token" };

                user.AccessToken = token;
                user.RefreshToken = refreshToken;

                _accountRepository.Update(user);
                var result = _accountRepository.Save();

                if (result == false) return new ResponseModel { Action = "Login", Code = "500", Error = "Can't save tokens in db" };

                return new ResponseModel
                {
                    Action = "Login",
                    Code = "200",
                    Token = token,
                    RefreshToken = refreshToken,
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

        public StatusModel Status(string token)
        {
            try
            {
                if (token == null) return new StatusModel { Code = "400", Action = "Status", Error = "Empty token" };

                var claims = _tokenService.ReadClaims(token);

                var accountRole = claims.FirstOrDefault(c => c.Type == "Role").Value;
                var accountEmail = claims.FirstOrDefault(c => c.Type == "Login").Value;

                return new StatusModel
                {
                    Code = "200",
                    Action = "Status",
                    Role = accountRole,
                    User = accountEmail
                };
            }
            catch (Exception)
            {
                return new StatusModel
                {
                    Code = "500",
                    Action = "Status",
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

                var user = new User
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

        public async Task<ResponseModel> Refresh(string token)
        {
            if (token == null) return new ResponseModel { Code = "400", Action = "Refresh", Error = "Empty token" };

            var validatedToken = _tokenService.ValidateToken(token);

            if (validatedToken == null) return new ResponseModel { Code = "400", Action = "Refresh", Error = "Outdated token" };

            var user = await _accountRepository.GetByRefreshToken(token);

            if (user == null) return new ResponseModel { Code = "500", Action = "Refresh", Error = "Empty user" };

            var accessToken = _tokenService.CreateAccessToken(user);

            if (accessToken == null) return new ResponseModel { Code = "500", Action = "Refresh", Error = "Empty access token" };

            user.AccessToken = accessToken;

            var refreshToken = _tokenService.CreateRefreshToken(user);

            if (refreshToken == null) return new ResponseModel { Code = "500", Action = "Refresh", Error = "Empty refresh token" };

            user.RefreshToken = refreshToken;

            _accountRepository.Update(user);
            var result = _accountRepository.Save();

            if (result == false) return new ResponseModel { Code = "500", Action = "Refresh", Error = "Can't save tokens in db" };

            return new ResponseModel
            {
                Code = "200",
                Action = "Refresh",
                Token = accessToken,
                RefreshToken = refreshToken
            };  
        }
    }
}
