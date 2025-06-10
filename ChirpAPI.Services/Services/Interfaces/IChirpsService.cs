using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChirpAPI.Models;

namespace ChirpAPI.Services.Services.Interfaces
{
    public interface IChirpsService
    {
        Task<List<ChirpViewModel>> GetChirpsByFilter(ChirpFilter filter);

    }
}
