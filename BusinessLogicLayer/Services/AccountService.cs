using AutoMapper;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataTransferObject;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, ITokenService tokenService, IAccountRepository accountRepository, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> Login(LoginDTO loginRequest)
        {
            var model = _mapper.Map<LoginModel>(loginRequest);

            var user = await _userManager.FindByEmailAsync(model.Login);

            if (user == null) throw new IncorrectInputException("Login or password entered incorrectly");

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordCorrect) throw new IncorrectInputException("Login or password entered incorrectly");

            var token = _tokenService.CreateAccessToken(user);

            if (token == null) throw new CreateTokenException("access", "Can't create token");

            var refreshToken = _tokenService.CreateRefreshToken(user);

            if (refreshToken == null) throw new CreateTokenException("refresh", "Can't create token");

            user.AccessToken = token;
            user.RefreshToken = refreshToken;

            _accountRepository.Update(user);
            var result = _accountRepository.Save();

            if (result == false) throw new SaveDbException("Can't save tokens in db");

            return new ResponseDTO
            {
                Action = "Login",
                Code = "200",
                Token = token,
                RefreshToken = refreshToken,
                User = user.Email
            };
        }

        public StatusDTO Status(string token)
        {
            if (token == null) throw new EmptyTokenException("access", "Empty token");

            var claims = _tokenService.ReadClaims(token);

            var accountRole = claims.FirstOrDefault(c => c.Type == "Role").Value;
            var accountEmail = claims.FirstOrDefault(c => c.Type == "Login").Value;

            return new StatusDTO
            {
                Code = "200",
                Action = "Status",
                Role = accountRole,
                User = accountEmail
            };
        }

        public async Task<ResponseDTO> Register(RegisterDTO registerRequest)
        {
            var model = _mapper.Map<RegisterModel>(registerRequest);

            var exist = await _userManager.FindByEmailAsync(model.Login);

            if (exist != null) throw new LoginExistException("User with that login exist");

            var user = new User
            {
                UserName = model.Login,
                Email = model.Login
            };
            var isCreated = await _userManager.CreateAsync(user, model.Password);

            if (!isCreated.Succeeded) throw new IncorrectInputException("Login or password entered incorrectly");

            return new ResponseDTO
            {
                Action = "Registration",
                Code = "200"
            };
        }

        public async Task<ResponseDTO> Refresh(string token)
        {
            if (token == null) throw new EmptyTokenException("refresh", "Empty token");

            var validatedToken = _tokenService.ValidateToken(token);

            if (validatedToken == null) throw new OutdatedTokenException("refresh", "Outdated token");

            var user = await _accountRepository.GetByRefreshToken(token);

            if (user == null) throw new EmptyUserException("Empty user");

            var accessToken = _tokenService.CreateAccessToken(user);

            if (accessToken == null) throw new EmptyTokenException("access", "Empty token");

            user.AccessToken = accessToken;

            var refreshToken = _tokenService.CreateRefreshToken(user);

            if (refreshToken == null) throw new EmptyTokenException("refresh", "Empty token");

            user.RefreshToken = refreshToken;

            _accountRepository.Update(user);
            var result = _accountRepository.Save();

            if (result == false) throw new SaveDbException("Can't save tokens in db");

            return new ResponseDTO
            {
                Code = "200",
                Action = "Refresh",
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
