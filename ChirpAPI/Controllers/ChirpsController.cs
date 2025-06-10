using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChirpAPI.Models;
using ChirpAPI.Services.Services.Interfaces;
using ChirpAPI.Services;

namespace ChirpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChirpsController : ControllerBase
    {
        private readonly IChirpsService _chirpsService;
        private readonly ILogger<ChirpsController> _logger;

        public ChirpsController(IChirpsService  chirpsService)
        {
            _chirpsService = chirpsService;
        }

        // GET: api/Chirps
        //[HttpGet("all")]
        //public async Task<ActionResult<IEnumerable<Chirp>>> GetChirps()
        //{
        //    return await _context.Chirps.ToListAsync();
        //}

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

        //// GET: api/Chirps/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Chirp>> GetChirp([FromRoute]int id)
        //{
        //    var chirp = await _context.Chirps.FindAsync(id);

        //    if (chirp == null)
        //    {
        //        return NotFound();
        //    }

        //    return chirp;
        //}

        //// PUT: api/Chirps/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutChirp([FromRoute]int id, [FromBody] Chirp chirp)
        //{
        //    if (id != chirp.Id)
        //    {
        //        return BadRequest("id diversi");
        //    }

        //    _context.Entry(chirp).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ChirpExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Chirps
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Chirp>> PostChirp([FromRoute] int id, [FromBody] Chirp chirp)
        //{
        //    _context.Chirps.Add(chirp);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetChirp", new { id = chirp.Id }, chirp);
        //}

        //// DELETE: api/Chirps/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteChirp([FromRoute] int id)
        //{
        //    var chirp = await _context.Chirps.FindAsync(id);
        //    if (chirp == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Chirps.Remove(chirp);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ChirpExists( int id)
        //{
        //    return _context.Chirps.Any(e => e.Id == id);
        //}
    }
}
