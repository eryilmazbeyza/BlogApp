using System.Security.Cryptography;
using System.Text;

namespace Blog.Application.Common.Security;

public class Crypto
{
    public static string PasswordHash(string text)
    {
        using var md5Hash = MD5.Create();
        var sourceBytes = Encoding.UTF8.GetBytes(text);
        var hashBytes = md5Hash.ComputeHash(sourceBytes);
        var md5Hashed = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(md5Hashed));
        var sha256 = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        return sha256;
    }
}