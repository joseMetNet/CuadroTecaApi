using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.IMG
{
    public class IMGDTO
    {
        public IFormFile IMGType1 { get; set; }
        public IFormFile IMG2Type2 { get; set; }

    }
    public class IMGResponse
    {
        public int Id { get; set; }
        public string IMGType1 { get; set; }

    }
    public class IMGResponse2
    {
        public int Id { get; set; }
        public string IMGType2 { get; set; }

    }
    public class IMGResponse3
    {
        public int Id { get; set; }
        public string IMGType1 { get; set; }
        public string IMGType2 { get; set; }

    }
}
