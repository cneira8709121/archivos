using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Criptography routines.
/// Requires access to HttpContext and Session object.
/// </summary>
public class clsCryptoUtil
{
    private byte[] EncryptSalt = Encoding.ASCII.GetBytes("sjak9327781jd8ejw8");

    /// <summary>
    /// Determina la clave interna, compuesta por: El ID de la sesión + semilla constante + número del día del mes.
    /// </summary>
    String SharedSecret
    {
        get
        {
            string output =
              string.Format("{0}{1}",
              "9823jrcddnuwew",
              DateTime.Now.Day);
            return output;
        }
    }

    #region FORCE ENCRYPT


    /// <summary> 
    /// Encripta una cadena de texto pequeña.
    /// Depende del ID de sesión, no puede utilizarse para pasar información
    /// entre sesiones.
    /// </summary> 
    /// <param name="plainText">The text to encrypt.</param> 
    public string EncryptString(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            RegistroTraza.I.Registrar("clsCryptoUtil.cs ::: EncryptString() ::: Plain text is null or empty");
            throw new ArgumentNullException("plainText");
        }

        string outStr = null;                       // Encrypted string to return 
        RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data. 

        try
        {
            // generate the key from the shared secret and the salt 
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(SharedSecret, EncryptSalt);

            // Create a RijndaelManaged object 
            // with the specified key and IV. 
            aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            // Create a decrytor to perform the stream transform. 
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption. 
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream. 
                        swEncrypt.Write(plainText);
                    }
                }
                outStr = Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
        finally
        {
            // Clear the RijndaelManaged object. 
            if (aesAlg != null)
                aesAlg.Clear();
        }

        // Return the encrypted bytes from the memory stream. 
        return outStr.Replace("/", "_").Replace("+", ")");
    }

    /// <summary> 
    /// Decodifica una cadena codificada.
    /// Depende del ID de sesión, no puede utilizarse para pasar información
    /// entre sesiones.
    /// </summary> 
    /// <param name="cipherText">The text to decrypt.</param> 
    public string DecryptString(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
        {
            RegistroTraza.I.Registrar("clsCryptoUtil.cs ::: DecryptString() ::: CipherText is null or empty");
            throw new ArgumentNullException("cipherText");
        }

        cipherText = cipherText.Replace("_", "/").Replace(")", "+");

        // Declare the RijndaelManaged object 
        // used to decrypt the data. 
        RijndaelManaged aesAlg = null;

        // Declare the string used to hold 
        // the decrypted text. 
        string plaintext = null;

        try
        {
            // generate the key from the shared secret and the salt 
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(SharedSecret, EncryptSalt);

            // Create a RijndaelManaged object 
            // with the specified key and IV. 
            aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            // Create a decrytor to perform the stream transform. 
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for decryption.                 
            byte[] bytes = Convert.FromBase64String(cipherText);
            using (MemoryStream msDecrypt = new MemoryStream(bytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string. 
                        plaintext = srDecrypt.ReadToEnd();
                }
            }
        }
        finally
        {
            // Clear the RijndaelManaged object. 
            if (aesAlg != null)
                aesAlg.Clear();
        }

        return plaintext;
    }


    #endregion

    #region FIXED ENCRYPT

    const string SharedSecretFixed = "9823jrcddaeiounuwew";

    /// <summary> 
    /// Encripta una cadena de texto.
    /// No depende del ID de sesión, luego puede utilizarse para pasar información
    /// entre sesiones.
    /// </summary> 
    /// <param name="plainText">The text to encrypt.</param> 
    public string EncryptStringFixed(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            RegistroTraza.I.Registrar("clsCryptoUtil.cs ::: EncryptString() ::: Plain text is null or empty");
            throw new ArgumentNullException("plainText");
        }

        string outStr = null;                       // Encrypted string to return 
        RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data. 

        try
        {
            // generate the key from the shared secret and the salt 
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(SharedSecretFixed, EncryptSalt);

            // Create a RijndaelManaged object 
            // with the specified key and IV. 
            aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            // Create a decrytor to perform the stream transform. 
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption. 
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream. 
                        swEncrypt.Write(plainText);
                    }
                }
                outStr = Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
        finally
        {
            // Clear the RijndaelManaged object. 
            if (aesAlg != null)
                aesAlg.Clear();
        }

        // Return the encrypted bytes from the memory stream. 
        return outStr.Replace("/", "_").Replace("+", ")");
    }

    /// <summary> 
    /// Decodifica una cadena codificada.
    /// No depende del ID de sesión, luego puede utilizarse para pasar información
    /// entre sesiones.
    /// </summary> 
    /// <param name="cipherText">The text to decrypt.</param> 
    public string DecryptStringFixed(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
        {
            RegistroTraza.I.Registrar("clsCryptoUtil.cs ::: EncryptString() ::: Cipher text is null or empty");
            throw new ArgumentNullException("cipherText");
        }

        cipherText = cipherText.Replace("_", "/").Replace(")", "+");

        // Declare the RijndaelManaged object 
        // used to decrypt the data. 
        RijndaelManaged aesAlg = null;

        // Declare the string used to hold 
        // the decrypted text. 
        string plaintext = null;

        try
        {
            // generate the key from the shared secret and the salt 
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(SharedSecretFixed, EncryptSalt);

            // Create a RijndaelManaged object 
            // with the specified key and IV. 
            aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            // Create a decrytor to perform the stream transform. 
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for decryption.                 
            byte[] bytes = Convert.FromBase64String(cipherText);
            using (MemoryStream msDecrypt = new MemoryStream(bytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string. 
                        plaintext = srDecrypt.ReadToEnd();
                }
            }
        }
        finally
        {
            // Clear the RijndaelManaged object. 
            if (aesAlg != null)
                aesAlg.Clear();
        }

        return plaintext;
    }

    #endregion
}
