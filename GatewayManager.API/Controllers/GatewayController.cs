using Microsoft.AspNetCore.Mvc;
using GatewayManager.Domain.DTOs;
using GatewayManager.Domain.IServices;
using GatewayManager.Domain.Pagination.Filters;
using GatewayManager.Domain.Entities;


namespace GatewayManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        protected readonly IGatewayService _gatewayService;

        public GatewayController(IGatewayService gatewayService)
        {
            this._gatewayService = gatewayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _gatewayService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var result = await _gatewayService.GetById(id);
                if (result != null) return Ok(result);
                return NotFound("The Gateway doesn't exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(GatewayDTO gateway)
        {
            try
            {
                gateway.DateCreated = DateTime.Now;
                gateway.DateUpdated = DateTime.Now;
                var result = await _gatewayService.Create(gateway);
                return Ok("Gateway created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(GatewayDTO gateway)
        {
            try
            {
                await _gatewayService.Update(gateway);
                return Ok("Gateway updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await _gatewayService.GetById(id);
                if (result == null)
                    return NotFound("The Gateway doesn't exist");

                await _gatewayService.Delete(id);
                return Ok("Gateway deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPage")]
        public async Task<IActionResult> GePage([FromQuery] GatewayFilter filter)
        {
            try
            {
                filter = filter ?? new GatewayFilter();
                var result = await _gatewayService.GetPage(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
