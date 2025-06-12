using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChirpAPI.Models;
using ChirpAPI.Services.Services.Interfaces;
using ChirpAPI.Services.Model.DTOs;
using Microsoft.Extensions.Logging;

namespace ChirpAPI.Controllers
{
    [Route("api/chirps/{chirpId}/[controller]")]  // Route più RESTful
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(ICommentsService commentsService, ILogger<CommentsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _commentsService = commentsService ?? throw new ArgumentNullException(nameof(commentsService));
        }

        // GET: api/chirps/5/comments
        [HttpGet]
        public async Task<IActionResult> GetComments([FromRoute] int chirpId)
        {
            _logger.LogInformation("Getting comments for chirp {ChirpId}", chirpId);

            var comments = await _commentsService.GetCommentsByChirpId(chirpId);

            if (comments == null || !comments.Any())
            {
                _logger.LogInformation("No comments found for chirp {ChirpId}", chirpId);
                return NoContent();
            }

            return Ok(comments);
        }

        // GET: api/chirps/5/comments/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int chirpId, [FromRoute] int id)
        {
            _logger.LogInformation("Getting comment {CommentId} for chirp {ChirpId}", id, chirpId);

            var comment = await _commentsService.GetCommentById(id);

            if (comment == null || comment.ChirpId != chirpId)
            {
                _logger.LogInformation("Comment {CommentId} not found for chirp {ChirpId}", id, chirpId);
                return NotFound();
            }

            return Ok(comment);
        }

        // POST: api/chirps/5/comments
        [HttpPost]
        public async Task<IActionResult> PostComment([FromRoute] int chirpId,[FromBody] CommentCreateModel model)
        {
           
           

            _logger.LogInformation("Creating comment for chirp {ChirpId}", chirpId);

          

            var createdComment = await _commentsService.CreateComment(chirpId, model);

            return createdComment == null
                ? NotFound($"Chirp with id {chirpId} not found")
                : CreatedAtAction(
                    nameof(GetComment),
                    new { chirpId, id = createdComment.Id },
                    createdComment);
        }

        // PUT: api/chirps/5/comments/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(
            [FromRoute] int chirpId,
            [FromRoute] int id,
            [FromBody] CommentUpdateModel model)
        {
            _logger.LogInformation("Updating comment {CommentId} for chirp {ChirpId}", id, chirpId);

            var updatedComment = await _commentsService.UpdateComment(id, model);

            if (updatedComment == null || updatedComment.ChirpId != chirpId)
            {
                _logger.LogInformation("Comment {CommentId} not found for chirp {ChirpId}", id, chirpId);
                return NotFound();
            }

            return Ok(updatedComment);
        }

        // DELETE: api/chirps/5/comments/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(
            [FromRoute] int chirpId,
            [FromRoute] int id)
        {
            _logger.LogInformation("Deleting comment {CommentId} for chirp {ChirpId}", id, chirpId);

            var result = await _commentsService.DeleteComment(id);

            if (!result)
            {
                _logger.LogInformation("Comment {CommentId} not found for chirp {ChirpId}", id, chirpId);
                return NotFound();
            }

            return NoContent();
        }
    }
}