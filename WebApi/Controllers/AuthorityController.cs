using Microsoft.AspNetCore.Mvc;
using WebApi.Authority;

namespace WebApi.Controllers;

[ApiController]
public class AuthorityController: ControllerBase
{
    private readonly IConfiguration configuration;

    public AuthorityController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpPost("auth")]
    public IActionResult Authenticate([FromBody] AppCredential credential)
    {
        if (Authenticator.Authenticate(credential.ClientId, credential.Secret))
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(10);

            return Ok(new
            {
                access_token = Authenticator.CreateToken(credential.ClientId, expiresAt, configuration.GetValue<string>("SecretKey")),
                expires_at = expiresAt
            });
        }
        else
        {
            ModelState.AddModelError("Unauthorized", "Not authorized");
            var problemDetails = new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status401Unauthorized,
            };
            return new UnauthorizedObjectResult(problemDetails);
        }
    }
    
}