using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Ruv.Infrastructure.Crosscutting.Common
{
  public static class clsSecurity
  {

    /// <summary>
    /// Desencriptar una cadena.
    /// </summary>
    /// <param name="base64Input"></param>
    /// <returns></returns>
    public static string Decrypt(string base64Input, string clave)
    {
      byte[] encryptBytes = Convert.FromBase64String(base64Input);
      byte[] saltBytes = Encoding.UTF8.GetBytes(@"hhdfwh&%/hjgsdj%");

      // Our symmetric encryption algorithm
      AesManaged aes = new AesManaged();

      // We're using the PBKDF2 standard for password-based key generation
      if (clave == null) clave = @"\sdfhg/hghht%|";
      Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(clave, saltBytes);

      // Setting our parameters
      aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
      aes.KeySize = aes.LegalKeySizes[0].MaxSize;

      aes.Key = rfc.GetBytes(aes.KeySize / 8);
      aes.IV = rfc.GetBytes(aes.BlockSize / 8);

      // Now, decryption
      ICryptoTransform decryptTrans = aes.CreateDecryptor();

      // Output stream, can be also a FileStream
      MemoryStream decryptStream = new MemoryStream();
      CryptoStream decryptor = new CryptoStream(decryptStream, decryptTrans, CryptoStreamMode.Write);

      decryptor.Write(encryptBytes, 0, encryptBytes.Length);
      decryptor.Flush();
      decryptor.Close();

      // Showing our decrypted content
      byte[] decryptBytes = decryptStream.ToArray();
      string decryptedString = UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);

      return decryptedString;
    }

    /// <summary>
    /// Encriptar una cadena.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string Encrypt(string input, string clave)
    {
      string data = input;
      byte[] utfdata = UTF8Encoding.UTF8.GetBytes(data);
      byte[] saltBytes = UTF8Encoding.UTF8.GetBytes(@"hhdfwh&%/hjgsdj%");

      // Our symmetric encryption algorithm
      AesManaged aes = new AesManaged();

      // We're using the PBKDF2 standard for password-based key generation
      if (clave == null) clave = @"\sdfhg/hghht%|";
      Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(clave, saltBytes);

      // Setting our parameters
      aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
      aes.KeySize = aes.LegalKeySizes[0].MaxSize;
      aes.Key = rfc.GetBytes(aes.KeySize / 8);
      aes.IV = rfc.GetBytes(aes.BlockSize / 8);

      // Encryption
      ICryptoTransform encryptTransf = aes.CreateEncryptor();

      // Output stream, can be also a FileStream
      MemoryStream encryptStream = new MemoryStream();
      CryptoStream encryptor = new CryptoStream(encryptStream, encryptTransf, CryptoStreamMode.Write);

      encryptor.Write(utfdata, 0, utfdata.Length);
      encryptor.Flush();
      encryptor.Close();

      // Showing our encrypted content
      byte[] encryptBytes = encryptStream.ToArray();

      string encryptedString = Convert.ToBase64String(encryptBytes);
      return encryptedString;
    }

  }
}
