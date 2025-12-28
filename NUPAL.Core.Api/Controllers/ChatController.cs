using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUPAL.Core.Application.DTOs;
using NUPAL.Core.Application.Interfaces;

namespace NUPAL.Core.API.Controllers
{
    [ApiController]
    [Route("api/chat")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chat;

        public ChatController(IChatService chat)
        {
            _chat = chat;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] ChatSendRequestDto body, CancellationToken ct)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(studentId))
                return Unauthorized(new { error = "unauthorized" });

            try
            {
                var resp = await _chat.SendAsync(studentId, body, ct);
                return Ok(resp);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = "bad_request", message = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { error = "upstream_error", message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "server_error", message = ex.Message });
            }
        }
    }
}
