using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestoApp.Application.Auth;
using RestoApp.Domain.DTO;

namespace RestoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IRestoAuthService restoAuthService;
        private readonly ILogger<authController> logger;

        public authController(IRestoAuthService restoAuthService, ILogger<authController> logger)
        {
            this.restoAuthService = restoAuthService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("resto/register")]
        public async Task<IActionResult> RegisterResto([FromBody] RegisterRestoRequestDto registerRestoDTO)
        {
            var result = await restoAuthService.RegisterResto(registerRestoDTO);
            if (result != null)
            {
                return Ok(new { message = "Berhasil register" });
            }
            return BadRequest(new { message = result });
        }
    }
}
