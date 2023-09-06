namespace Api.Helpers;

/// <summary>
/// Хелпер для создания кодов, ключей и паролей
/// </summary>
public static class CodeHelper
{
    private static readonly Random _random = new Random();
    
    private const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
    private const string SpecialCharacters = "!@#$%^&*():;";
    private const string Digits = "0123456789";

    /// <summary>
    /// Генерация секретного ключа
    /// </summary>
    /// <param name="length">Длина ключа</param>
    /// <returns>Ключ</returns>
    public static string GenerateSecret(int length)
    {
        return new string(Generate(length, UpperCaseLetters + LowerCaseLetters + SpecialCharacters + Digits));
    }
    
    /// <summary>
    /// Генерация случайно строки
    /// </summary>
    /// <param name="length">Длина строки</param>
    /// <returns>Строка</returns>
    public static string GenerateString(int length)
    {
        return new string(Generate(length, UpperCaseLetters + LowerCaseLetters));
    }

    /// <summary>
    /// Генерация строки
    /// </summary>
    /// <param name="length">Длина строки</param>
    /// <param name="characters">Символы для генерации</param>
    /// <returns>Строка</returns>
    private static char[] Generate(int length, string characters)
    {
        char[] code = new char[length];
        
        for (int i = 0; i < length; i++)
        {
            code[i] = GetRandomCharacter(characters);
        }

        return code;
    }
    
    /// <summary>
    /// Получение случайного символа из строки
    /// </summary>
    /// <param name="input">Строка</param>
    /// <returns>Символ</returns>
    private static char GetRandomCharacter(string input)
    {
        return input[_random.Next(0, input.Length)];
    }
    
    /// <summary>
    /// Перемешивание строк кода
    /// </summary>
    /// <param name="array">Пароль</param>
    private static void Shuffle(char[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = _random.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}