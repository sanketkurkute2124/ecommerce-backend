using ECommerceApp.DTOs;
using ECommerceApp.DTOs.CancellationDTOs;
using ECommerceApp.Models;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;


namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancellationController : Controller
    {
        private readonly CancellationService _cancellationService;
        public CancellationController(CancellationService cancellationService)
        {
            _cancellationService = cancellationService;
        }

        [HttpPost("RequestCancellation")]
        public async Task<ActionResult<ApiResponse<CancellationResponseDTO>>> RequestCancellationAsync([FromBody] CancellationRequestDTO cancellationRequest)
        {
            var response = await _cancellationService.RequestCancellationAsync(cancellationRequest);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpGet("GetCancellationById")]
        public async Task<ActionResult<ApiResponse<CancellationResponseDTO>>> GetCancellationByIdAsync(int id)
        {
            var response = await _cancellationService.GetCancellationByIdAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }


        [HttpPost("UpdateCancellationStatus")]
        public async Task<ActionResult<ApiResponse<CancellationResponseDTO>>> UpdateCancellationStatusAsync([FromBody] CancellationStatusUpdateDTO statusUpdate)
        {
            var response = await _cancellationService.UpdateCancellationStatusAsync(statusUpdate);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }


        [HttpGet("GetAllCancellations")]
        public async Task<ActionResult<ApiResponse<List<CancellationResponseDTO>>>> GetAllCancellationsAsync()
        {
            var response = await _cancellationService.GetAllCancellationsAsync();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }


        [HttpGet("NotifyCancellationAccepted")]
        private async Task<ActionResult> NotifyCancellationAcceptedAsync(Cancellation cancellation)
        {
            var response = await _cancellationService.GetAllCancellationsAsync();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

    }
}
