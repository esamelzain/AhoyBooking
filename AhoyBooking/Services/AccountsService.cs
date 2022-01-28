using AhoyBooking.Models;
using AhoyBooking.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AhoyBooking.Services
{
    public interface IAccountsService
    {
        Task<BaseResponse> AddUser(RegisterModel model);
        Task<LoginResponseModel> Login(LoginModel model);
    }
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        public AccountsService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            _configuration = configuration;
        }
        public async Task<BaseResponse> AddUser(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new BaseResponse
                {
                    Message = new ResponseMessage { Code = 441, Message = "User Exists" }
                };
            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new BaseResponse
                {
                    Message = new ResponseMessage { Code = 512, Message = "Sorry, Invalid User Information" }
                };
            return new BaseResponse
            {
                Message = new ResponseMessage { Code = 200, Message = "Successful" }
            };
        }
        public async Task<LoginResponseModel> Login(LoginModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                List<Claim> authClaims = new()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("userId", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return new LoginResponseModel
                {
                    Message = new ResponseMessage { Code = 200, Message = "Successful" },
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            return new LoginResponseModel
            {
                Message = new ResponseMessage { Code = 404, Message = "Login Failed, Please check your credentials!" }
            };
        }
    }
}
