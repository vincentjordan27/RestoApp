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
        private readonly ICustomerAuthService customerAuthService;

        public authController(IRestoAuthService restoAuthService, ILogger<authController> logger , ICustomerAuthService customerAuthService)
        {
            this.restoAuthService = restoAuthService;
            this.logger = logger;
            this.customerAuthService = customerAuthService;
        }

        [HttpPost]
        [Route("register/resto")]
        public async Task<IActionResult> RegisterResto([FromBody] RegisterRestoRequestDto registerRestoDTO)
        {
            var result = await restoAuthService.RegisterResto(registerRestoDTO);
            if (result == null)
            {
                return Ok(new { message = "Berhasil register" });
            }
            return BadRequest(new { message = result });
        }

        [HttpPost]
        [Route("login/resto")]
        public async Task<IActionResult> LoginResto([FromBody] LoginRequestDto loginRequest)
        {
            var result = await restoAuthService.LoginResto(loginRequest);
            if (result != null)
            {
                return Ok(new { token = result });
            }
            return BadRequest(new { message = "Username or password incorrect" });
        }

        [HttpPost]
        [Route("register/customer")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerRequestDto requestDto)
        {
            var result = await customerAuthService.RegisterCustomer(requestDto);
            if (result == null)
            {
                return Ok(new { message = "Berhasil register" });
            }
            return BadRequest(new { message = result });
        }

        [HttpPost]
        [Route("login/customer")]
        public async Task<IActionResult> LoginCustomer([FromBody] LoginRequestDto loginRequest)
        {
            var result = await customerAuthService.LoginCustomer(loginRequest);
            if (result != null)
            {
                return Ok(new { token = result });
            }
            return BadRequest(new { message = "Username or password incorrect" });
        }

    }
}
