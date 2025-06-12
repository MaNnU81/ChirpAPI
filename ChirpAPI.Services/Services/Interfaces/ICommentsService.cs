using ChirpAPI.Services.Model.DTOs;
using ChirpAPI.Services.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.Services.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<CommentViewModel?> GetCommentById(int id);
        Task<List<CommentViewModel>> GetCommentsByChirpId(int chirpId);
        Task<CommentViewModel?> CreateComment(int id,  CommentCreateModel model);
        Task<CommentViewModel?> UpdateComment(int id, CommentUpdateModel model);
        Task<bool> DeleteComment(int id);
    }
}
