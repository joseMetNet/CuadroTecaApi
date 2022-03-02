using fotoTeca.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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


namespace fotoTeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 

    public class CuentasApiController : ControllerBase
    {
        private readonly UserManager<AplicationUser> _userManager;
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public CuentasApiController(
            UserManager<AplicationUser> userManager,
            SignInManager<AplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Crear")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new AplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return BuildToken(model);
            }
            else
            {
                return BadRequest("Username or password invalid");
            }

        }

//        {
//  "email": "adminTeca@gmail.com",
//  "password": "Qwer1234!"
//}

//        {
//  "email": "adminTC@gmail.com",
//  "password": "AaSD12!23"
//}
    [HttpPost("Actualizar")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<UserToken>> UpdateUser([FromBody] UserInfoUpdate model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            if (result.Succeeded)
            {
                UserInfo userInfo = new UserInfo();
                userInfo.Email = model.Email;
                userInfo.Password = model.NewPassword;

                return BuildToken(userInfo);
            }
            else
            {
                return BadRequest("usuario o password invalido");
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(userInfo);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                return BadRequest(ModelState);
            }
        }

        private UserToken BuildToken(UserInfo userInfo)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
        new Claim("miValor", "Lo que yo quiera"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            // var expiration = DateTime.UtcNow.AddHours(1);
            var expiration = DateTime.Now.AddDays(1);


            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
