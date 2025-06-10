using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChirpAPI.Models;
using ChirpAPI.Services.Model;

namespace ChirpAPI.Services.Services.Interfaces
{
    public interface IChirpsService
    {
        Task<List<ChirpViewModel>> GetChirpsByFilter(ChirpFilter filter);
        Task<ChirpViewModel> CreateChirp(ChirpCreateModel chirpCreateModel);
        Task<List<ChirpViewModel>> GetAllChirps();
        Task<ChirpViewModel?> GetChirpById(int id);
        Task<ChirpViewModel?> UpdateChirp(int id, ChirpUpdateModel chirpUpdateModel);
        Task<bool> DeleteChirp(int id);


    }
}
