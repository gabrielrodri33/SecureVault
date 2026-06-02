namespace SecureVault.Application.Common.Interfaces;
public interface ICryptoService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}
