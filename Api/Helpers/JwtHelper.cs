using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Api.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helpers;

/// <summary>
/// Помощник для работы с Jwt
/// </summary>
public static class JwtHelper
{
    /// <summary>
    /// Создание настроек jwt
    /// </summary>
    /// <param name="filePath">Путь к файлу настроек для записи</param>
    /// <returns>Модель настроек приложения</returns>
    private static JwtSettings CreateJwtSettings(string filePath)
    {
        // Создание экземпляра настроек jwt
        JwtSettings jwtSettings = new JwtSettings();
        
        // Сериализация в json
        var content = JsonSerializer.Serialize( new { JwtSettings = jwtSettings });
        
        // Запись в файл
        File.WriteAllText(filePath, content);

        return jwtSettings;
    }

    /// <summary>
    /// Получение настроек jwt
    /// </summary>
    /// <param name="settingsFilePath">Путь к файлу настроек</param>
    /// <returns>Модель настроек приложения</returns>
    public static JwtSettings GetJwtSettings(string settingsFilePath)
    {
        try
        {
            var content = File.ReadAllText(settingsFilePath);
            var jwtConfig = JsonSerializer.Deserialize<Dictionary<string, JwtSettings>>(content);
            
            return jwtConfig != null 
                ? jwtConfig["JwtSettings"] 
                : CreateJwtSettings(settingsFilePath);
        }
        catch (Exception e)
        {
            return CreateJwtSettings(settingsFilePath);
        }
    }
    
    /// <summary>
    /// Генерация активного токена
    /// </summary>
    /// <param name="jwtSettings">Настройки Jwt</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Активный токен</returns>
    public static string GenerateAccessToken(JwtSettings jwtSettings, Guid userId)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("id", userId.ToString())
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    /// <summary>
    /// Генерация токена обновления
    /// </summary>
    /// <returns>Токен обновления</returns>
    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[256];
        
        using (var rng = RandomNumberGenerator.Create()) 
            rng.GetBytes(randomNumber);
        
        return Convert.ToBase64String(randomNumber);
    }
}