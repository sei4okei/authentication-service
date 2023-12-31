﻿using AuthenticationService.Models;
using AuthenticationService.Models.DTOs;
using AuthenticationService.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var loginModel = _mapper.Map<LoginModel>(loginDTO);

            var result = await _accountService.Login(loginModel);

            var resultDTO = _mapper.Map<ResponseDTO>(result);

            if (result.Error != null)
            {
                return BadRequest(resultDTO);
            }

            return Ok(resultDTO);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var registerModel = _mapper.Map<RegisterModel>(registerDTO);

            var result = await _accountService.Register(registerModel);

            var resultDTO = _mapper.Map<ResponseDTO>(result);

            if (result.Error != null)
            {
                return BadRequest(resultDTO);
            }

            return Ok(resultDTO);
        }
    }
}
