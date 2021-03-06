using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Weathery.API.Models.Identity;
using Weathery.Data.Models;
using Weathery.Infrastructure.Configurations;

namespace Weathery.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationSettingsConfiguration _appSettings;

        public IdentityController(UserManager<User> userManager,
            IOptions<ApplicationSettingsConfiguration> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route(nameof(Register))]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Login to the application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route(nameof(Login))]
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if(user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!passwordValid)
            {
                return Unauthorized();
            }

              var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
