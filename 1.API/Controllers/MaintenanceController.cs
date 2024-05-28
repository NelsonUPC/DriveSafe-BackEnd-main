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
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceData _maintenanceData;
        private readonly IMaintenanceDomain _maintenanceDomain;
        private readonly IMapper _mapper;
        
        public MaintenanceController(IMaintenanceData maintenanceData,IMaintenanceDomain maintenanceDomain,IMapper mapper)
        {
            _maintenanceData = maintenanceData;
            _maintenanceDomain = maintenanceDomain;
            _mapper = mapper;
        }
        
        // GET: api/Maintenance
        [HttpGet]
        public async Task<IActionResult>  GetAsync()
        {
            var data = await _maintenanceData.getAllAsync();
            var result = _mapper.Map<List<Maintenance>,List<MaintenanceResponse>>(data);
            return Ok(result);
        }

        // POST: api/Maintenance
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] MaintenanceRequest data)
        {
            try
           {
               if (!ModelState.IsValid) return BadRequest();
               
               var tutorial = _mapper.Map<MaintenanceRequest, Maintenance>(data);
               
               var result = await _maintenanceDomain.SaveAsync(tutorial);

               //TODO
               return Created("api/Maintenance", result);
           }
           catch (Exception ex)
           {
               //loggear txt,base,cloud 
               return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
           }
        }
    }
}
