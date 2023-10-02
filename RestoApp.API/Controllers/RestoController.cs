using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestoApp.Application.Auth;
using RestoApp.Application.Resto;
using RestoApp.Domain.Constant;
using RestoApp.Domain.DTO;

namespace RestoApp.API.Controllers
{
    [Route("api/")]
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

        [HttpPost("menu")]
        [Authorize]
        public async Task<IActionResult> AddRestoMenu([FromBody] MenuDto addMenuDto)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var userId = tokenService.GetUserId(token);
            var result = await restoService.AddMenu(addMenuDto, userId);
            if (result.Status == Constant.ERROR)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("menu/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateMenu([FromBody] MenuDto menuDto, Guid id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var userId = tokenService.GetUserId(token);
            var result = await restoService.UpdateMenu(menuDto, id, userId);
            if (result.Status == Constant.ERROR)
            {
                return BadRequest(result);
            } else
            {
                return Ok(result);
            }
        }

        [HttpDelete("menu/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var userId = tokenService.GetUserId(token);
            var response = await restoService.DeleteMenu(id, userId);
            if (response.Status == Constant.ERROR)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
