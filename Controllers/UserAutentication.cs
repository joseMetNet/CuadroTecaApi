using fotoTeca.Autentication;
using fotoTeca.Models.UserAutentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserAutentication : ControllerBase
    {
        private readonly AplicationDbContex context;
        private IConfiguration _ConnectionString;
        private UserManager<fotoTeca.Models.AplicationUser> _userManager;
        public UserAutentication(AplicationDbContex vcontext, IConfiguration Configuration, UserManager<fotoTeca.Models.AplicationUser> userManager)
        {
            this.context = vcontext;
            this._ConnectionString = Configuration;
            _userManager = userManager;
        }


        [HttpPost("/AutenticarUser", Name = "AuntenticarUser")]
        public async Task<ActionResult<Usuario>> Post([FromBody] LoginUserDTO Login)
        {
            Usuario usuario = new Usuario();
            try
            {
                LoginUserDAL objLogueo = new LoginUserDAL(_ConnectionString.GetConnectionString("DefaultConnection"));
                usuario = objLogueo.AutenticarUsuario(Login);
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).Name;
                var user = await _userManager.FindByNameAsync(userId);


            }
            catch (Exception ex)
            {
                //usuario.Result.Valor = 500;
                //usuario.Result.Mensaje = ex.Message;
                return usuario;
            }
            return usuario;
        }


    }
}
