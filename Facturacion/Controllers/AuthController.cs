using Facturacion.DTOs;
using Facturacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Facturacion.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController(IAuthService service) : ControllerBase
  {
    private readonly IAuthService _service = service;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto request)
    {
      var user = await _service.Register(request);
      if (user == null) return BadRequest(new { Message = "El usuario ya existe" });
      return Ok(new { Message = "Usuario registrado exitosamente", User = user });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(CreateUserDto request)
    {
      var tokenResponse = await _service.Login(request);
      if (tokenResponse == null) return Unauthorized(new { Message = "Credenciales inválidas" });
      return Ok(new { Message = "Inicio de sesión exitoso", Tokens = tokenResponse });
    }

    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordDto updatePasswordDto)
    {
      var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
      var updated = await _service.UpdatePassword(userId, updatePasswordDto);
      if (!updated) return BadRequest(new { Message = "Contraseña actual incorrecta o usuario no registrado" });
      return Ok(new { Message = "Contraseña actualizada exitosamente" });
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
    {
      var tokenResponse = await _service.GetTokenResponse(request);
      if (tokenResponse == null) return Unauthorized(new { Message = "Token de actualización inválido" });
      return Ok(new { Message = "Token actualizado exitosamente", Tokens = tokenResponse });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
      var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
      var result = await _service.Logout(userId);
      if (!result) return NotFound(new { message = "Usuario no encontrado" });
      return Ok(new { message = "Sesión cerrada correctamente" });
    }
  }
}
