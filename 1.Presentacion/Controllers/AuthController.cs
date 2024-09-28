using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Usuario usuario)
    {
        Console.WriteLine(usuario);
        if (usuario == null)
        {
            return BadRequest(new { message = "Datos de usuario no proporcionados." });
        }

        // Verificar si el correo ya está en uso
        if (await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo))
        {
            return BadRequest(new { message = "El correo ya está en uso" });
        }

        // Hashear la contraseña
        usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

        try
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok("Registro exitoso");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, new { message = "Error al registrar el usuario. Por favor, inténtalo de nuevo más tarde." });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Correo == login.Correo);
        
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Contraseña, usuario.Contraseña))
        {
            return Unauthorized();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]); // Aquí obtenemos la clave desde IConfiguration
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { Token = tokenHandler.WriteToken(token) });
    }
}

public class LoginDto
{
    public string Correo { get; set; }
    public string Contraseña { get; set; }
}
