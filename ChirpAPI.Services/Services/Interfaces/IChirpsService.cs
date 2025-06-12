using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChirpAPI.Services.Model.DTOs;
using ChirpAPI.Services.Model.Filters;
using ChirpAPI.Services.Model.ViewModel;

namespace ChirpAPI.Services.Services.Interfaces
{
    public interface IChirpsService
    {
        Task<List<ChirpViewModel>> GetChirpsByFilter(ChirpFilter filter);
        Task<int?> CreateChirp(ChirpCreateModel chirpCreateModel);
        Task<List<ChirpViewModel>> GetAllChirps();
        Task<ChirpViewModel?> GetChirpById(int id);
        Task<int?> UpdateChirp(int id, ChirpUpdateModel chirpUpdateModel);
        Task<int?> DeleteChirp(int id);


    }
}
