using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.API.Request;
using _1.API.Response;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioData _usuarioData;
        private readonly IUsuarioDomain _usuarioDomain;
        private readonly IMapper _mapper;
        
        public UsuarioController(IUsuarioData usuarioData,IUsuarioDomain usuarioDomain, IMapper mapper)
        {
            _usuarioData = usuarioData;
            _usuarioDomain = usuarioDomain;
            _mapper = mapper;
        }
        
        // GET: api/Usuario
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _usuarioData.getAllAsync();
            var result = _mapper.Map<List<Usuario>,List<UsuarioResponse>>(data);
            return Ok(result);
        }
        // POST: api/Usuario
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UsuarioRequest data)
        {
            try
           {
               if (!ModelState.IsValid) return BadRequest();
               
               var tutorial = _mapper.Map<UsuarioRequest, Usuario>(data);
               
               var result = await _usuarioDomain.SaveAsync(tutorial);

               //TODO
               return Created("api/Usuario", result);
           }
           catch (Exception ex)
           {
               //loggear txt,base,cloud 
               return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
           }
        }
    }
}
