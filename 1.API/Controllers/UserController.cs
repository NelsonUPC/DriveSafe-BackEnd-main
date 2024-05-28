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
    public class UserController : ControllerBase
    {
        private readonly IUserData _userData;
        private readonly IUserDomain _userDomain;
        private readonly IMapper _mapper;
        
        public UserController(IUserData userData,IUserDomain userDomain, IMapper mapper)
        {
            _userData = userData;
            _userDomain = userDomain;
            _mapper = mapper;
        }
        
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _userData.getAllAsync();
            var result = _mapper.Map<List<User>,List<UserResponse>>(data);
            return Ok(result);
        }
        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserRequest data)
        {
            try
           {
               if (!ModelState.IsValid) return BadRequest();
               
               var tutorial = _mapper.Map<UserRequest, User>(data);
               
               var result = await _userDomain.SaveAsync(tutorial);

               //TODO
               return Created("api/User", result);
           }
           catch (Exception ex)
           {
               //loggear txt,base,cloud 
               return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
           }
        }
    }
}
