using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication1.Models.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ClinicController(IDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _dbService.GetDoctors();
            return Ok(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctors(SomeSortOfDoctors someSortOfDoctors)
        {
            await _dbService.AddDoctor(someSortOfDoctors);
            return Ok("Dodano pomyslnie");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctors(int id)
        {
            try
            {
                await _dbService.DeleteDoctor(id);
                return Ok($"Pomyslnie usunieto doktora o id {id}");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("doctor/{id}")]
        public async Task<IActionResult> UpdateDoctors(SomeSortOfDoctors someSortOfDoctors, int id)
        {
            try
            {
                await _dbService.UpdateDoctor(someSortOfDoctors, id);
                return Ok("Pomyslnie zmodyfikowano");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("perscription/{id}")]
        public async Task<IActionResult> GetPrescription(int id)
        {
            try
            {
                var perscription = await _dbService.GetPerscription(id);
                return Ok(perscription);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
