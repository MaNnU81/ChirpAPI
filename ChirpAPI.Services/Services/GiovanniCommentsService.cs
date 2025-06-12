using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChirpAPI.Models;
using ChirpAPI.Services.Services.Interfaces;

namespace ChirpAPI.Services.Services
{
    internal class GiovanniCommentsService : ICommentsService
    {

        private readonly CinguettioContext _context;
        public GiovanniCommentsService(CinguettioContext context)
        {
            _context = context;
        }





    }
}
