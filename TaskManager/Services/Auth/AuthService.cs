
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Contracts.Auth;
using TaskManager.Models;

namespace TaskManager.Services.Auth;
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    public static User user = new User();

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public LoginResponse Login(LoginRequest data)
    {
        //TODO: Validar que exista usuario por uid o correo
        // return BadRequest("User not found.")

        // si la contraseño con coincide
        if (!BCrypt.Net.BCrypt.Verify(data.contraseña, user.contraseña))
        {
            throw new Exception();
        }

        string token = GenerateToken(user.Uid);
        user.token = token;

        return new LoginResponse(user.Uid, user.nombre, user.token);
    }

    public User Register(CreateUserRequest newUser)
    {

        string contrasenaCifrada = BCrypt.Net.BCrypt.HashPassword(newUser.contraseña);

        //TODO: genera JWT
        user = new User(newUser.nombre, newUser.correo, contrasenaCifrada);
        string token = GenerateToken(user.Uid);
        user.token = token;

        // Guardar usuario DB

        return user;
    }

    private string GenerateToken(Guid uid)
    {
        var claims = new[] { new Claim("uid", uid.ToString()) };
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        var creds =  new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );
        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return  token;
    }

     public int? ValidateToken(string token)
    {
        if (token == null) 
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }

}