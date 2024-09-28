using Microsoft.AspNetCore.Mvc;
using GatewayManager.Domain.DTOs;
using GatewayManager.Domain.IServices;
using GatewayManager.Domain.Pagination.Filters;
using GatewayManager.Domain.Entities;


namespace GatewayManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeripheralDeviceController : ControllerBase
    {
        protected readonly IPeripheralDeviceService _peripheralDeviceService;

        public PeripheralDeviceController(IPeripheralDeviceService peripheralDeviceService)
        {
            this._peripheralDeviceService = peripheralDeviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _peripheralDeviceService.GetAll();
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
                var result = await _peripheralDeviceService.GetById(id);
                if (result != null) return Ok(result);
                return NotFound("The Peripheral Device doesn't exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeripheralDeviceDTO peripheralDevice)
        {
            try
            {
                peripheralDevice.DateCreated = DateTime.Now;
                peripheralDevice.DateUpdated = DateTime.Now;
                var result = await _peripheralDeviceService.Create(peripheralDevice);
                return Ok("Peripheral Device created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(PeripheralDeviceDTO peripheralDevice)
        {
            try
            {
                peripheralDevice.Status ??= "offline";
                await _peripheralDeviceService.Update(peripheralDevice);
                return Ok("Peripheral Device updated successfully");
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
                var result = await _peripheralDeviceService.GetById(id);
                if (result == null)
                    return NotFound("The Peripheral Device doesn't exist");

                await _peripheralDeviceService.Delete(id);
                return Ok("Peripheral Device deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPage")]
        public async Task<IActionResult> GePage([FromQuery] PeripheralDeviceFilter filter)
        {
            try
            {
                filter = filter ?? new PeripheralDeviceFilter();
                var result = await _peripheralDeviceService.GetPage(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
