using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestoApp.Application.Resto;
using RestoApp.Domain.Constant;

namespace RestoApp.API.Controllers
{
    [Route("api/")]
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

        [HttpGet]
        [Route("{id}/menu")]
        [Authorize]
        public async Task <IActionResult> GetAllRestoMenu(Guid id)
        {
            var result = await restoService.GetRestoMenu(id);
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
