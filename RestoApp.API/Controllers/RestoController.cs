using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestoApp.Application.Auth;
using RestoApp.Application.Resto;
using RestoApp.Domain.DTO;

namespace RestoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestoController : ControllerBase
    {
        private readonly IRestoService restoService;
        private readonly ITokenService tokenService;

        public RestoController(IRestoService restoService, ITokenService tokenService)
        {
            this.restoService = restoService;
            this.tokenService = tokenService;
        }

        [HttpGet("menu")]
        [Authorize]
        public async Task<IActionResult> GetMyRestoMenu()
        {
            
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var userId = tokenService.GetUserId(token);
            var result = await restoService.GetRestoMenu(userId);
            var wrapper = new
            {
                datas = result.datas,
                message = result.message,
                status = result.status
            };
            if (result.message != null)
            {
                return BadRequest(wrapper);
            } 
            else
            {
                return Ok(wrapper);
            }
        }
    }
}
