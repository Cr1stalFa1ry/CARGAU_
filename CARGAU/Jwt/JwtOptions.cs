using System;

namespace CARGAU.Jwt;

public class JwtOptions
{
    public string SecretKey { get; set; } = String.Empty;
    public int ExpiteMinutes { get; set; }
}
