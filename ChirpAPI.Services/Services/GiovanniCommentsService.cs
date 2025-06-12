using ChirpAPI.Models;
using ChirpAPI.Services.Model.DTOs;
using ChirpAPI.Services.Model.ViewModel;
using ChirpAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChirpAPI.Services.Services
{
    public class GiovanniCommentsService : ICommentsService
    {
        private readonly CinguettioContext _context;

        public GiovanniCommentsService(CinguettioContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

       

        public async Task<CommentViewModel?> CreateComment(int chirpId, CommentCreateModel model)
        {
            // Verifica che il Chirp esista
            if (!await _context.Chirps.AnyAsync(c => c.Id == chirpId))
            {
                return null;
            }

            var comment = new Comment
            {
                Text = model.Text,
                ChirpId = chirpId, // Usa l'ID dalla route
                CreationTime = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return new CommentViewModel
            {
                Id = comment.Id,
                Text = comment.Text,
                CreationTime = comment.CreationTime,
                ChirpId = comment.ChirpId
            };
        }

    

        public async Task<bool> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CommentViewModel?> GetCommentById(int id)
        {
            return await _context.Comments
                .Where(c => c.Id == id)
                .Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreationTime = c.CreationTime,
                    ChirpId = c.ChirpId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<CommentViewModel>> GetCommentsByChirpId(int chirpId)
        {
            return await _context.Comments
                .Where(c => c.ChirpId == chirpId)
                .OrderByDescending(c => c.CreationTime)
                .Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreationTime = c.CreationTime,
                    ChirpId = c.ChirpId
                })
                .ToListAsync();
        }

        public async Task<CommentViewModel?> UpdateComment(int id, CommentUpdateModel model)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            // Aggiorna solo i campi modificabili
            existingComment.Text = model.Text;

            try
            {
                await _context.SaveChangesAsync();

                return new CommentViewModel
                {
                    Id = existingComment.Id,
                    Text = existingComment.Text,
                    CreationTime = existingComment.CreationTime,
                    ChirpId = existingComment.ChirpId
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CommentExists(id))
                {
                    return null;
                }
                throw;
            }
        }

        private async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.AnyAsync(e => e.Id == id);
        }
    }
}