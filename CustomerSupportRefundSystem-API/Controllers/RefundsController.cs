using CustomerSupportRefundSystem_API.Models;
using CustomerSupportRefundSystem_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportRefundSystem_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RefundsController : ControllerBase
{
    private readonly IRefundService _refundService;

    public RefundsController(IRefundService refundService)
    {
        _refundService = refundService;
    }

    [HttpPost]
    public async Task<ActionResult<RefundResponse>> Create(
        CreateRefundRequest request)
    {
        try
        {
            var result = await _refundService.CreateAsync(request);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<RefundResponse>>> GetAll()
    {
        return Ok(await _refundService.GetAllAsync());
    }
}
