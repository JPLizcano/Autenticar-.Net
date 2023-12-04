using CertificarConToken.Models;
using CertificarConToken.Models.Custom;
using CertificarConToken.Models.Customs;
using CertificarConToken.Services.Custom;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CertificarConToken.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly DbPruebaContext _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(DbPruebaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerarToken(string idUsuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));
            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256
                );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialesToken
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string tokenCreado = tokenHandler.WriteToken(tokenConfig);
            return tokenCreado;
        }

        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = _context.Usuarios.FirstOrDefault(x =>
            x.NombreUsuarios == autorizacion.NombreUsuario && x.Clave == autorizacion.Clave);
            if (usuario_encontrado == null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }
            string tokenCreado = GenerarToken(usuario_encontrado.IdUsuario.ToString());
            return new AutorizacionResponse() { Token = tokenCreado, Resultado = true, Mensaje = "Ok" };
        }
    }
}
