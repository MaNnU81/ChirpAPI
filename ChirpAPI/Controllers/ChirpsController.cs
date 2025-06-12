using ChirpAPI.Services.Model.DTOs;
using ChirpAPI.Services.Model.Filters;
using ChirpAPI.Services.Model.ViewModel;
using ChirpAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChirpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChirpsController : ControllerBase
    {
        private readonly IChirpsService _chirpsService;
        private readonly ILogger<ChirpsController> _logger;

        public ChirpsController(IChirpsService chirpsService, ILogger<ChirpsController> logger)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _chirpsService = chirpsService ?? throw new ArgumentNullException(nameof(chirpsService));
        }

        // GET: api/Chirps
        [HttpGet("all")]
        public async Task<IActionResult> GetAllChirps()
        {
            _logger.LogInformation("ChirpsController.GetAllChirps called");

            var chirps = await _chirpsService.GetAllChirps();

            if (chirps == null || !chirps.Any())
            {
                _logger.LogInformation("No chirps found in database");
                return NoContent();
            }

            _logger.LogInformation("Returning {Count} chirps", chirps.Count);
            return Ok(chirps);
        }

        // GET: api/Chirps?text= testo da cercare
        [HttpGet]
        public async Task<IActionResult> GetChirpsByFilter([FromQuery] ChirpFilter filter)
        {
            _logger.LogInformation("ChirpsController.GetChirpsByFilter called with filter: {@Filter}", filter);

            List<ChirpViewModel> result = await _chirpsService.GetChirpsByFilter(filter);



            if (result == null || !result.Any())
            {
                _logger.LogInformation("No chirps found for the given filter: {@Filter}", filter);
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Chirps found: {@Chirps}", result);
                return Ok(result);
            }
        }

        //// GET: api/Chirps/5   ricerca by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChirpById([FromRoute] int id)
        {
            _logger.LogInformation("ChirpsController.GetChirpById called with id: {Id}", id);

            var chirp = await _chirpsService.GetChirpById(id);

            if (chirp == null)
            {
                _logger.LogInformation("Chirp with id {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Returning chirp with id {Id}", id);
            return Ok(chirp);
        }

        //// PUT: api/Chirps/5  put per aggiornare un chirp
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChirp([FromRoute] int id, [FromBody] ChirpUpdateModel chirpUpdateModel)
        {
            _logger.LogInformation("ChirpsController.PutChirp called for id: {Id} with data: {@Chirp}", id, chirpUpdateModel);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID provided: {Id}", id);
                return BadRequest("Invalid ID");
            }

            var updatedChirp = await _chirpsService.UpdateChirp(id, chirpUpdateModel);

            if (updatedChirp == null)
            {
                _logger.LogInformation("Chirp with id {Id} not found for update", id);
                return NotFound();
            }

            _logger.LogInformation("Chirp with id {Id} successfully updated", id);
            return Ok(updatedChirp);
        }

        // POST: api/Chirps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostChirp([FromBody] ChirpCreateModel chirpCreateModel)
        {
            try
            {
                _logger.LogInformation("ChirpsController.PostChirp called with chirp: {@Chirp}", chirpCreateModel);

                

                var chirpId = await _chirpsService.CreateChirp(chirpCreateModel);

                if (chirpId == null)
                {
                    return BadRequest("Text obbligatorio");
                }



                return Created($"/api/chirps/{chirpId}", chirpId);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating chirp");
                return StatusCode(500, "Internal server error");
            }
        }

        //// DELETE: api/Chirps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChirp([FromRoute] int id)
        {
            _logger.LogInformation("ChirpsController.DeleteChirp called for id: {Id}", id);


            int? result = await _chirpsService.DeleteChirp(id);

            if (result == null)
            {
                _logger.LogInformation("Chirp with id {Id} not found for deletion", id);
                return BadRequest("chirp non esistente");
            }

            if (result == -1 )
            {
                return BadRequest("eliminare prima i commenti");
            }
            return Ok(result);
        }
    }
}
