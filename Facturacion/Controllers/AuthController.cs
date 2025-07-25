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
      var token = await _service.Login(request);
      if (token == null) return Unauthorized(new { Message = "Credenciales inválidas" });
      return Ok(new { Message = "Inicio de sesión exitoso", Token = token });
    }

    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordDto updatePasswordDto)
    {
      var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
      var updated = await _service.UpdatePassword(userId, updatePasswordDto);
      if (!updated) return BadRequest(new { Message = "Contraseña actual incorrecta o usuario no registrado" });
      return Ok(new { Message = "Contraseña actualizada exitosamente" });
    }
  }
}
