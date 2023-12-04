using System;
using System.Collections.Generic;

namespace CertificarConToken.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuarios { get; set; }

    public string? Clave { get; set; }
}
