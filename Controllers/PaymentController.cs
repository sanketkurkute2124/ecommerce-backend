using ECommerceApp.DTOs;
using ECommerceApp.DTOs.PaymentDTOs;
using ECommerceApp.Models;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ECommerceApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController:Controller
    {
        private readonly PaymentService _paymentService;
        public PaymentController(PaymentService paymentService)
        {
            _paymentService=paymentService;
        }

        [HttpPost("ProcessPayment")]
        public async Task<ActionResult<ApiResponse<PaymentResponseDTO>>> ProcessPaymentAsync([FromBody]PaymentRequestDTO paymentRequest)
        {
            var response= await _paymentService.ProcessPaymentAsync(paymentRequest);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }


        [HttpGet("GetPaymentById")]
        public async Task<ActionResult<ApiResponse<PaymentResponseDTO>>> GetPaymentByIdAsync(int paymentId)
        {
            var response = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        
        [HttpGet("GetPaymentByOrderId")]
        public async Task<ActionResult<ApiResponse<PaymentResponseDTO>>> GetPaymentByOrderIdAsync(int orderId)
        {
            var response = await _paymentService.GetPaymentByOrderIdAsync(orderId);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPut("UpdatePaymentStatus")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdatePaymentStatusAsync([FromBody] PaymentStatusUpdateDTO statusUpdate)
        {

            var response = await _paymentService.UpdatePaymentStatusAsync(statusUpdate);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }


        [HttpPost("CompleteCODPayment")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> CompleteCODPaymentAsync([FromBody]CODPaymentUpdateDTO codPaymentUpdateDTO)
        {
            var response= await _paymentService.CompleteCODPaymentAsync(codPaymentUpdateDTO);

            if(response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode,response);
            }
            return Ok(response);
        }
    }
}
