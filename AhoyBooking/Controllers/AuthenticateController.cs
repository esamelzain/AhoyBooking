using AhoyBooking.Models;
using AhoyBooking.Services;
using AhoyBooking.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAccountsService _accountService;
        public AuthenticateController(IAccountsService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginModel model) => Ok(await _accountService.Login(model));
        [HttpPost("Register")]
        public async Task<ActionResult<BaseResponse>> Register(RegisterModel registerModel) => Ok(await _accountService.AddUser(registerModel));
    }
}
