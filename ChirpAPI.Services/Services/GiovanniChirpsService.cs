using ChirpAPI.Models;
using ChirpAPI.Services.Model;
using ChirpAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.Services.Services
{
    public class GiovanniChirpsService : IChirpsService
    {

        private readonly CinguettioContext _context;
        public GiovanniChirpsService(CinguettioContext context)
        {
            _context = context;
        }


        public async Task<List<ChirpViewModel>> GetChirpsByFilter(ChirpFilter filter)
        {

            IQueryable<Chirp> query = _context.Chirps.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(x => x.Text == filter.Text);

            }
            var result = await query.Select(x => new ChirpViewModel
            {
                Id = x.Id,
                Text = x.Text,
                ExtUrl = x.ExtUrl,
                CreationTime = x.CreationTime,
                Lat = x.Lat,
                Lng = x.Lng,

            }).ToListAsync();

            return result;

        }




        public async Task<ChirpViewModel> CreateChirp(ChirpCreateModel chirpCreateModel)
        {
            var chirp = new Chirp
            {
                Text = chirpCreateModel.Text,
                ExtUrl = chirpCreateModel.ExtUrl,
                Lat = chirpCreateModel.Lat,
                Lng = chirpCreateModel.Lng,

            };

            _context.Chirps.Add(chirp);
            await _context.SaveChangesAsync();

            return new ChirpViewModel
            {
                Id = chirp.Id,
                Text = chirp.Text,
                ExtUrl = chirp.ExtUrl,
                CreationTime = chirp.CreationTime,
                Lat = chirp.Lat,
                Lng = chirp.Lng
            };
        }

        public async Task<List<ChirpViewModel>> GetAllChirps()
        {
            return await _context.Chirps
                //.OrderByDescending(c => c.CreationTime) // Ordina per data decrescente (modificabile)
                .Select(c => new ChirpViewModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    ExtUrl = c.ExtUrl,
                    CreationTime = c.CreationTime,
                    Lat = c.Lat,
                    Lng = c.Lng
                })
                .ToListAsync();
        }


        public async Task<ChirpViewModel?> GetChirpById(int id)
        {
            return await _context.Chirps
                .Where(c => c.Id == id)
                .Select(c => new ChirpViewModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    ExtUrl = c.ExtUrl,
                    CreationTime = c.CreationTime,
                    Lat = c.Lat,
                    Lng = c.Lng
                })
                .FirstOrDefaultAsync();
        }



        public async Task<ChirpViewModel?> UpdateChirp(int id, ChirpUpdateModel chirpUpdateModel)
        {
            var existingChirp = await _context.Chirps.FindAsync(id);

            if (existingChirp == null)
            {
                return null;
            }

            // Aggiorna solo i campi modificabili
            existingChirp.Text = chirpUpdateModel.Text;
            existingChirp.ExtUrl = chirpUpdateModel.ExtUrl;
            existingChirp.Lat = chirpUpdateModel.Lat;
            existingChirp.Lng = chirpUpdateModel.Lng;

            try
            {
                await _context.SaveChangesAsync();

                return new ChirpViewModel
                {
                    Id = existingChirp.Id,
                    Text = existingChirp.Text,
                    ExtUrl = existingChirp.ExtUrl,
                    CreationTime = existingChirp.CreationTime,
                    Lat = existingChirp.Lat,
                    Lng = existingChirp.Lng
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Chirps.AnyAsync(c => c.Id == id))
                {
                    return null;
                }
                throw;
            }
        }

        public async Task<bool> DeleteChirp(int id)
        {
            var chirp = await _context.Chirps.FindAsync(id);
            if (chirp == null)
            {
                return false;
            }

            _context.Chirps.Remove(chirp);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
