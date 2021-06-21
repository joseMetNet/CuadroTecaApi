using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.User
{
    public class UserDTOrequired
    {
        public int idUser   { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int idTypeUser { get; set; }
        public string email { get; set; }
        public string password { get; set; }
  
    }
    public class RecoverPassworResponse
    {
        public string api_dice { get; set; }
        public int idUser { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string idTypeUser { get; set; }
        public string email { get; set; }
        public string idStatusUser { get; set; }
    }
    public class RecoverPassworRequeride
    {
        public string email { get; set; }

    }
    public class UserResponse 
    {
        public int idUser { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int idTypeUser { get; set; }
        public string email { get; set; }
        public int idStatus { get; set; }
    
    }

}
