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
            var data = await _rentData.GetAllAsync();
            var result = _mapper.Map<List<Rent>,List<RentResponse>>(data);
            return Ok(result);
        }
        
        //GET: api/Rent/5
        [HttpGet("{id}", Name = "GetRentById")]
        public async Task<IActionResult> GetAsyncById(int id)
        {
            var data = await _rentData.GetByIdAsync(id);
            var result = _mapper.Map<Rent,RentResponse>(data);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        //GET: api/Rent/GetByUserId/5
        [HttpGet("GetByUserId/{id}")]
        public async Task<IActionResult> GetByUserIdAsync(int id)
        {
            var data = await _rentData.GetByUserIdAsync(id);
            var result = _mapper.Map<List<Rent>,List<RentResponse>>(data);
            if (result == null) return NotFound();
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
               return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
           }
        }
        
        // PUT: api/Rent/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] RentRequest data)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                
                var rent = _mapper.Map<RentRequest, Rent>(data);
                
                var result = await _rentDomain.UpdateAsync(rent, id);
                
                if (!result) return NotFound();
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        
        // DELETE: api/Rent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _rentDomain.DeleteAsync(id);
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
