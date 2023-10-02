using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestoApp.Application.Resto;
using RestoApp.Domain.Constant;

namespace RestoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRestoService restoService;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(IRestoService restoService, ILogger<CustomerController> logger)
        {
            this.restoService = restoService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("resto")]
        [Authorize]
        public async Task <IActionResult> GetAllRestos()
        {
            var response = await restoService.GetListResto();
            if (response.Status == Constant.ERROR)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
