using Microsoft.AspNetCore.Mvc;
using DecorHaven.API.DTOs.Common;
using DecorHaven.API.DTOs.Newsletter;
using DecorHaven.API.Services;

namespace DecorHaven.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsletterController : ControllerBase
{
    private readonly INewsletterService _newsletterService;

    public NewsletterController(INewsletterService newsletterService)
    {
        _newsletterService = newsletterService;
    }

    [HttpPost("subscribe")]
    public async Task<ActionResult<ApiResponse<bool>>> Subscribe([FromBody] NewsletterSubscriptionDto subscriptionDto)
    {
        var result = await _newsletterService.SubscribeAsync(subscriptionDto);

        if (!result)
        {
            return BadRequest(ApiResponse<bool>.ErrorResponse("Subscription failed"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Successfully subscribed to newsletter"));
    }

    [HttpPost("unsubscribe")]
    public async Task<ActionResult<ApiResponse<bool>>> Unsubscribe([FromBody] string email)
    {
        var result = await _newsletterService.UnsubscribeAsync(email);

        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("Subscription not found"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Successfully unsubscribed from newsletter"));
    }
}

