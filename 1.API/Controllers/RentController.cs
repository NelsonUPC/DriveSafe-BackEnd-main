using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.API.Request;
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
    public class RentController : ControllerBase
    {
        private readonly IRentData _rentData;
        private readonly IRentDomain _rentDomain;
        private readonly IMapper _mapper;
        
        public RentController(IRentData rentData, IRentDomain rentDomain, IMapper mapper)
        {
            _rentData = rentData;
            _rentDomain = rentDomain;
            _mapper = mapper;
        }
        
        // GET: api/Rent
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _rentData.getAllAsync();
            var result = _mapper.Map<List<Rent>,List<Rent>>(data);
            return Ok(result);
        }

        // POST: api/Rent
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RentRequest data)
        {
            try
           {
               if (!ModelState.IsValid) return BadRequest();
               
               var rent = _mapper.Map<RentRequest, Rent>(data);
               
               var result = await _rentDomain.SaveAsync(rent);

               //TODO
               return Created("api/Rent", result);
           }
           catch (Exception ex)
           {
               //loggear txt,base,cloud 
               return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
           }
        }
    }
}
