using Empo.EmployeeService.Application.AuthService.ForgotPassword;
using Empo.EmployeeService.Application.AuthService.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Route("login")]
    [HttpPost]
    [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var response = await _mediator.Send(LoginCommand.Create(model));
        return Ok(response);
    }

    [Route("forgot-password")]
    [HttpPatch]
    [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.Created)]

    public async Task<IActionResult> UpdatePassword([FromBody] ForgotPasswordRequest model)
    {
        var response = await _mediator.Send(ForgotPasswordCommand.Create(model));
        return Ok(response);
    }
}
