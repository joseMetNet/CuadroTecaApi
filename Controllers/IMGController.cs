using AutoMapper;
using fotoTeca.Autentication;
using fotoTeca.Models.IMG;
using fotoTeca.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IMGController : ControllerBase
    {

        private readonly IMGDAL _repository1;
        private readonly AplicationDbContex context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "files";

        public IMGController(IMGDAL repository1, AplicationDbContex contex, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));
            this.context = contex;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpPost("/SaveIMG")]
        public async Task<ActionResult> Post2([FromForm] IMGDTO req)
        {
            var entidad = mapper.Map<IMGdataTable>(req);

            if (req.IMGType1 != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await req.IMGType1.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extencion = Path.GetExtension(req.IMGType1.FileName);
                    entidad.IMG1 = await almacenadorArchivos.GuardarArchivo(contenido, extencion, contenedor, req.IMGType1.ContentType);
                }
            }
            if (req.IMG2Type2 != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await req.IMG2Type2.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extencion = Path.GetExtension(req.IMG2Type2.FileName);
                    entidad.IMG2 = await almacenadorArchivos.GuardarArchivo(contenido, extencion, contenedor, req.IMG2Type2.ContentType);
                }
            }

            context.Add(entidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        //GET-----------------------------------------------------------------------------------------------------------------------------

        [HttpGet("/GetIMGType1")]
        public async Task<List<IMGResponse>> get1()
        {
            return await _repository1.GetIMGType1();

        }
        [HttpGet("/GetIMGType1/{idIMG}")]
        public async Task<List<IMGResponse>> get11(int idIMG)
        {
            return await _repository1.GetIMGType1(idIMG);
        }

        [HttpGet("/GetIMGType2")]
        public async Task<List<IMGResponse2>> get2()
        {
            return await _repository1.GetIMGType2();

        }
        [HttpGet("/GetIMGType2/{idIMG}")]
        public async Task<List<IMGResponse2>> get22(int idIMG)
        {
            return await _repository1.GetIMGType2(idIMG);
        }
        [HttpGet("/GetIMGTotality")]
        public async Task<List<IMGResponse3>> get3()
        {
            return await _repository1.GetIMGTotality();

        }
        [HttpGet("/GetIMGTotality/{idIMG}")]
        public async Task<List<IMGResponse3>> get33(int idIMG)
        {
            return await _repository1.GetIMGTotality(idIMG);
        }
    }
}
