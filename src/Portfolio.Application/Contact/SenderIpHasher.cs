using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Application.Contact;

/// <summary>
/// Превращает IP отправителя в необратимый идентификатор.
/// Нужен, чтобы ограничивать поток сообщений, не сохраняя сам адрес.
/// </summary>
internal static class SenderIpHasher
{
    public static string Hash(string ip, string salt)
    {
        var bytes = Encoding.UTF8.GetBytes($"{salt}|{ip}");
        return Convert.ToHexString(SHA256.HashData(bytes));
    }
}
