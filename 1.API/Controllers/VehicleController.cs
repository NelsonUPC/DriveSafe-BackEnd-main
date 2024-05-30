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
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleData _vehicleData;
        private readonly IVehicleDomain _vehicleDomain;
        private readonly IMapper _mapper;
        
        public VehicleController(IVehicleData vehicleData,IVehicleDomain vehicleDomain,IMapper mapper)
        {
            _vehicleData = vehicleData;
            _vehicleDomain = vehicleDomain;
            _mapper = mapper;
        }
        
        // GET: api/Vehicle
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _vehicleData.GetAllAsync();
            var result = _mapper.Map<List<Vehicle>,List<VehicleResponse>>(data);
            return Ok(result);
        }
        
        // GET: api/Vehicle/5
        [HttpGet("{id}", Name = "GetVehicleById")]
        public async Task<IActionResult> GetAsyncById(int id)
        {
            var data = await _vehicleData.GetByIdAsync(id);
            var result = _mapper.Map<Vehicle,VehicleResponse>(data);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        // POST: api/Vehicle
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] VehicleRequest data)
        {
            try
           {
               if (!ModelState.IsValid) return BadRequest();
               
               var vehicle = _mapper.Map<VehicleRequest, Vehicle>(data);
               
               var result = await _vehicleDomain.SaveAsync(vehicle);

               //TODO
               return Created("api/Vehicle", result);
           }
           catch (Exception ex)
           {
               //loggear txt,base,cloud 
               return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
           }
        }
    }
}
