using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	[HttpGet("userinfo")]
	[Authorize] // Ensures only authenticated users can access this endpoint
	public IActionResult GetUserInfo()
	{
		var user = HttpContext.User;
		if (user?.Identity?.IsAuthenticated ?? false)
		{
			return Ok(new
			{
				Username = user.Identity.Name,
				AuthenticationType = user.Identity.AuthenticationType
			});
		}
		else
		{
			return Unauthorized("User is not authenticated");
		}
	}

	[HttpGet("public")]
	[AllowAnonymous] // Allows public access without authentication
	public IActionResult GetPublicInfo()
	{
		return Ok("This is a public endpoint");
	}
}
