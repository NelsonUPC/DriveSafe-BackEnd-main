using _1.API.Request;
using _1.API.Response;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
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
            var data = await _userData.GetAllAsync();
            var result = _mapper.Map<List<User>,List<UserResponse>>(data);
            return Ok(result);
        }
        
        //GET: api/User/5
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetAsyncById(int id)
        {
            var data = await _userData.GetByIdAsync(id);
            var result = _mapper.Map<User,UserResponse>(data);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserRequest data)
        {
            try
           {
               if (!ModelState.IsValid) return BadRequest();
               
               var user = _mapper.Map<UserRequest, User>(data);
               
               var result = await _userDomain.SaveAsync(user);

               //TODO
               return Created("api/User", result);
           }
           catch (Exception ex)
           {
               return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
           }
        }
        
        //PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UserRequest data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.name) ||
                    string.IsNullOrEmpty(data.last_name) ||
                    data.birthdate == default ||
                    data.cellphone == 0 ||
                    string.IsNullOrEmpty(data.gmail) ||
                    string.IsNullOrEmpty(data.password) ||
                    string.IsNullOrEmpty(data.type))
                {
                    return BadRequest("All fields are required");
                }
                if (!ModelState.IsValid) return BadRequest();
                
                var user = _mapper.Map<UserRequest, User>(data);
                
                var result = await _userDomain.UpdateAsync(user, id);
                
                if (!result) return NotFound();
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        
        //DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _userDomain.DeleteAsync(id);
                if (!result) return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
    }
}
