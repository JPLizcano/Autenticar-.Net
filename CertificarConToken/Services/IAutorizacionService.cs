using CertificarConToken.Models.Custom;
using CertificarConToken.Models.Customs;

namespace CertificarConToken.Services.Custom
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
    }
}
