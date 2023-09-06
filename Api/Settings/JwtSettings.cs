using Api.Helpers;

namespace Api.Settings;

/// <summary>
/// Модель настроек Jwt
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Признак проверки издателя
    /// </summary>
    public bool ValidateIssuer { get; set; } = true;
    
    /// <summary>
    /// Признак проверки аудитории
    /// </summary>
    public bool ValidateAudience { get; set; } = true;
    
    /// <summary>
    /// Признак проверки срока действия
    /// </summary>
    public bool ValidateLifetime { get; set; } = true;
    
    /// <summary>
    /// Признак проверки ключа
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; } = true;

    /// <summary>
    /// Издатель
    /// </summary>
    public string ValidIssuer { get; set; } = CodeHelper.GenerateString(10);
    
    /// <summary>
    /// Аудитория
    /// </summary>
    public string ValidAudience { get; set; } = CodeHelper.GenerateString(10);

    /// <summary>
    /// Секретный ключ
    /// </summary>
    public string SecretKey { get; set; } = CodeHelper.GenerateSecret(256);
    
}