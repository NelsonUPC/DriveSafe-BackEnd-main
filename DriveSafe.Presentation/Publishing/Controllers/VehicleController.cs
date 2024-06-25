using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;
using DriveSafe.Domain.Publishing.Services;
using DriveSafe.Presentation.Publishing.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSafe.Presentation.Publishing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVehicleCommandService _vehicleCommandService;
        private readonly IVehicleQueryService _vehicleQueryService;
        
        public VehicleController(IVehicleQueryService vehicleQueryService, IVehicleCommandService vehicleCommandService, IMapper mapper)
        {
            _vehicleQueryService = vehicleQueryService;
            _vehicleCommandService = vehicleCommandService;
            _mapper = mapper;
        }
        
        // GET: api/Vehicle
        [HttpGet]
        [ProducesResponseType(typeof(List<VehicleResponse>), 200)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        [AuthorizeCustom("admin", "tenant")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _vehicleQueryService.Handle(new GetAllVehiclesQuery());
            
            return Ok(result);
        }

        // GET: api/Vehicle/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(VehicleResponse), 200)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
        [AuthorizeCustom("admin")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _vehicleQueryService.Handle(new GetVehicleByIdQuery(id));
            
            if (result==null) StatusCode(StatusCodes.Status404NotFound);

            return Ok(result);
        }

        // POST: api/Vehicle
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
        [AuthorizeCustom("admin","tenant")]
        public async Task <IActionResult> PostAsync([FromBody] CreateVehicleCommand command)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var result = await _vehicleCommandService.Handle(command);
            
            return StatusCode( StatusCodes.Status201Created, result);
        }

        // PUT: api/Vehicle/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
