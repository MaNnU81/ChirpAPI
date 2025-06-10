using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChirpAPI.Models;
using ChirpAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChirpAPI.Services.Services
{
    public class GiovanniChirpsService: IChirpsService
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


        }   }

    
}
