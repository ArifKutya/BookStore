using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IConfiguration _configuration;

        public UserInfoController(
            IUserInfoService userInfoService,
            IConfiguration configuration)
        {
            _userInfoService = userInfoService;
            _configuration = configuration;
        }

        [ProducesResponseType(StatusCodes.Status200OK,
            Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
                return BadRequest("Wrong e-mail and/or password");

            var user =
                await _userInfoService.GetUserInfoAsync(email, password);

            if (user != null)
            {
                //var userRoles = await _identityService.GetUserRoles(user);

                //create claims details based on the user information
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt:Subject").Value),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("DisplayName", user.Username ?? string.Empty),
                    new Claim("View", "Employee"),
                };

                //foreach (var role in userRoles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, role));
                //}

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }

        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task Add([FromBody] AddUserInfoRequest user)
        {
            await _userInfoService.Add(user);
        }
    }
}
