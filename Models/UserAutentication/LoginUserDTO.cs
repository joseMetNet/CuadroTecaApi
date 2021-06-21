using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.UserAutentication
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "El usuario es obllicatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        public string password { get; set; }
    }
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public int idTypeUser { get; set; }
        public string nombreCompleto { get; set; }
        public bool Valido { get; set; }
        public Resultado Result { get; set; }

    }
}
