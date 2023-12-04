using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CertificarConToken.Models.Custom;
using CertificarConToken.Services;
using CertificarConToken.Controllers;
using CertificarConToken.Services.Custom;

namespace CertificarConToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAutorizacionService _autorizacionService;
        public UsuarioController(IAutorizacionService autorizacionService)
        {
            _autorizacionService = autorizacionService;
        }
        [HttpGet]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            var resultado_autorizacion = await _autorizacionService.DevolverToken(autorizacion);
            if (resultado_autorizacion == null)
                return Unauthorized();
            return Ok(resultado_autorizacion);
        }
    }
}
