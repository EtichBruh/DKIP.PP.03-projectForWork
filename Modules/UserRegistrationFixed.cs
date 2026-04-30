using System.Text.RegularExpressions;

namespace ReviewSamples.Modules;

public record RegistrationResult(bool IsSuccess, string Message);

public class UserRegistrationFixed
{
    private const int MinLoginLength = 3;
    private const int MinPasswordLength = 8;

    public RegistrationResult Register(string login, string password, string email)
    {
        if (string.IsNullOrWhiteSpace(login) || login.Length < MinLoginLength)
            return new RegistrationResult(false, "Логин должен содержать минимум 3 символа.");

        if (string.IsNullOrWhiteSpace(password) || password.Length < MinPasswordLength)
            return new RegistrationResult(false, "Пароль должен содержать минимум 8 символов.");

        if (!IsValidEmail(email))
            return new RegistrationResult(false, "Email указан некорректно.");

        return new RegistrationResult(true, "Пользователь зарегистрирован.");
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
