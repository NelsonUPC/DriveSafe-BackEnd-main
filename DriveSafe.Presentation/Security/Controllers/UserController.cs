using System.Net.Mime;
using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;
using DriveSafe.Domain.Publishing.Services;
using DriveSafe.Domain.Security.Models.Commands;
using DriveSafe.Presentation.Publishing.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSafe.Presentation.Publishing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserCommandService _userCommandService;
        private readonly IUserQueryService _userQueryService;
        
        public UserController(IUserQueryService userQueryService, IUserCommandService userCommandService, IMapper mapper)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
            _mapper = mapper;
        }
        
        // GET: api/User
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(List<UserResponse>), 200)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userQueryService.Handle(new GetAllUsersQuery());
    
            return Ok(result);
        }
        
        // GET: api/User/Id
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        [AuthorizeCustom( "admin", "tenant", "owner")]
        public async Task<IActionResult> GetAsyncById(int id)
        {
            var result = await _userQueryService.Handle(new GetUserByIdQuery(id));
            
            if (result == null) return NotFound();
    
            return Ok(result);
        }
        
        // POST: api/User/Register
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserResponse), 201)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromBody] SignUpCommand command)
        {
            var result = await _userCommandService.Handle(command);
            return Ok(result);
        }
        
        // POST: api/User/Login
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> LoginAsync([FromBody] SignInCommand command)
        {
            var result = await _userCommandService.Handle(command);
            return Ok(result);
        }
        
        // POST: api/User
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), 201)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserCommand command)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
    
                var user = await _userCommandService.Handle(command);
                
                return CreatedAtRoute("GetUserById", new { id = user }, user);
            }
            catch (Exception ex)
            {
                // Log the inner exception if it exists
                if (ex.InnerException != null)
                {
                    // Replace this with your preferred logging method
                    Console.WriteLine(ex.InnerException);
                }
                
                return StatusCode(500, ex.Message);
            }
        }
        
        // PUT: api/User/Id
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        [AuthorizeCustom( "admin", "tenant", "owner")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateUserCommand command)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);

            var result = await _userCommandService.Handle(id, command);
            
            return Ok();
        }
        
        
        // DELETE: api/User/Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            DeleteUserCommand command = new DeleteUserCommand { Id = id };
            
            var result = await _userCommandService.Handle(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
