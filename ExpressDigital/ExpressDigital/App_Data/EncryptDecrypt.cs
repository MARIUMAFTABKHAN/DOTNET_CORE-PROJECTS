using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptDecrypt
{
    public static string GetSqlConnection(string connectionStringName = "DefaultConnection")
    {
        // optionally defaults to "DefaultConnection" if no connection string name is inputted
        string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        string passPhrase = "passphrase-used-to-encrypt";
        // decrypt password
        string password = get_prase_after_word(connectionString, "password=", ";");
        //connectionString = connectionString.Replace(password, EncryptDecrypt.Decrypt(password, EncryptDecrypt.passPhrase, EncryptDecrypt.saltValue, EncryptDecrypt.hashAlgorithm, EncryptDecrypt.passwordIterations, EncryptDecrypt.initVector, EncryptDecrypt.keySize));
        if (IsBase64String(password))
        {
            string decryptedPassword = EncryptDecrypt.Decrypt(password,
                EncryptDecrypt.passPhrase, EncryptDecrypt.saltValue,
                EncryptDecrypt.hashAlgorithm, EncryptDecrypt.passwordIterations,
                EncryptDecrypt.initVector, EncryptDecrypt.keySize);

            connectionString = connectionString.Replace(password, decryptedPassword);
        }

        return connectionString;
    }
    public static bool IsBase64String(string base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
            return false;

        base64 = base64.Trim();

        if (base64.Length % 4 != 0)
            return false;

        try
        {
            Convert.FromBase64String(base64);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string get_prase_after_word(string search_string_in, string word_before_in, string word_after_in)
    {
        int myStartPos = 0;
        string myWorkString = "";
        // get position where phrase "word_before_in" ends
        if (!string.IsNullOrEmpty(word_before_in))
        {
            myStartPos = search_string_in.ToLower().IndexOf(word_before_in) + word_before_in.Length;
            // extract remaining text
            myWorkString = search_string_in.Substring(myStartPos, search_string_in.Length - myStartPos).Trim();
            if (!string.IsNullOrEmpty(word_after_in))
            {
                // get position where phrase starts in the working string
                myWorkString = myWorkString.Substring(0, myWorkString.IndexOf(word_after_in)).Trim();
            }
        }
        else
        {
            myWorkString = string.Empty;
        }
        return myWorkString.Trim();
    }

    #region "Encrypt Decrypt"

    public static String passPhrase = "Pas5pr@sedsfksdf4rd;slkf@#$#@$@#$)_+(*&&^^%%$$#@#DSDFG";        // can be any string
    public static String saltValue = "(*&^&%$#@@%%GHHGFKKLKLG@!~$%^&*()_+2136879875s@1tValue";        // can be any string
    public static String hashAlgorithm = "SHA1";             // can be "MD5"
    public static Int32 passwordIterations = 2;                  // can be any number
    public static String initVector = "$#@%^&*()*&^%$#@"; // must be 16 bytes //@1B2c3D4e5F6g7H8
    public static Int32 keySize = 256;

    public static String Encrypt(string plainText,
                                 string passPhrase,
                                 string saltValue,
                                 string hashAlgorithm,
                                 int passwordIterations,
                                 string initVector,
                                 int keySize)
    {
        // Convert strings into byte arrays.
        // Let us assume that strings only contain ASCII codes.
        // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
        // encoding.
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

        // Convert our plaintext into a byte array.
        // Let us assume that plaintext contains UTF8-encoded characters.
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        // First, we must create a password, from which the key will be derived.
        // This password will be generated from the specified passphrase and 
        // salt value. The password will be created using the specified hash 
        // algorithm. Password creation can be done in several iterations.
        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);

        // Use the password to generate pseudo-random bytes for the encryption
        // key. Specify the size of the key in bytes (instead of bits).
        byte[] keyBytes = password.GetBytes(keySize / 8);

        // Create uninitialized Rijndael encryption object.
        RijndaelManaged symmetricKey = new RijndaelManaged();

        // It is reasonable to set encryption mode to Cipher Block Chaining
        // (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC;

        // Generate encryptor from the existing key bytes and initialization 
        // vector. Key size will be defined based on the number of the key 
        // bytes.
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                         keyBytes,
                                                         initVectorBytes);

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream();

        // Define cryptographic stream (always use Write mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                     encryptor,
                                                     CryptoStreamMode.Write);
        // Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

        // Finish encrypting.
        cryptoStream.FlushFinalBlock();

        // Convert our encrypted data from a memory stream into a byte array.
        byte[] cipherTextBytes = memoryStream.ToArray();

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        // Convert encrypted data into a base64-encoded string.
        string cipherText = Convert.ToBase64String(cipherTextBytes);

        // Return encrypted string.
        return cipherText;
    }

    /// <summary>
    /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    /// </summary>
    /// <param name="cipherText">
    /// Base64-formatted ciphertext value.
    /// </param>
    /// <param name="passPhrase">
    /// Passphrase from which a pseudo-random password will be derived. The
    /// derived password will be used to generate the encryption key.
    /// Passphrase can be any string. In this example we assume that this
    /// passphrase is an ASCII string.
    /// </param>
    /// <param name="saltValue">
    /// Salt value used along with passphrase to generate password. Salt can
    /// be any string. In this example we assume that salt is an ASCII string.
    /// </param>
    /// <param name="hashAlgorithm">
    /// Hash algorithm used to generate password. Allowed values are: "MD5" and
    /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    /// </param>
    /// <param name="passwordIterations">
    /// Number of iterations used to generate password. One or two iterations
    /// should be enough.
    /// </param>
    /// <param name="initVector">
    /// Initialization vector (or IV). This value is required to encrypt the
    /// first block of plaintext data. For RijndaelManaged class IV must be
    /// exactly 16 ASCII characters long.
    /// </param>
    /// <param name="keySize">
    /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
    /// Longer keys are more secure than shorter keys.
    /// </param>
    /// <returns>
    /// Decrypted string value.
    /// </returns>
    /// <remarks>
    /// Most of the logic in this function is similar to the Encrypt
    /// logic. In order for decryption to work, all parameters of this function
    /// - except cipherText value - must match the corresponding parameters of
    /// the Encrypt function which was called to generate the
    /// ciphertext.
    /// </remarks>
    public static String Decrypt(string cipherText,
                                 string passPhrase,
                                 string saltValue,
                                 string hashAlgorithm,
                                 int passwordIterations,
                                 string initVector,
                                 int keySize)
    {
        // Convert strings defining encryption key characteristics into byte
        // arrays. Let us assume that strings only contain ASCII codes.
        // If strings include Unicode characters, use Unicode, UTF7, or UTF8
        // encoding.
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

        // Convert our ciphertext into a byte array.
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

        // First, we must create a password, from which the key will be 
        // derived. This password will be generated from the specified 
        // passphrase and salt value. The password will be created using
        // the specified hash algorithm. Password creation can be done in
        // several iterations.
        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);

        // Use the password to generate pseudo-random bytes for the encryption
        // key. Specify the size of the key in bytes (instead of bits).
        byte[] keyBytes = password.GetBytes(keySize / 8);

        // Create uninitialized Rijndael encryption object.
        RijndaelManaged symmetricKey = new RijndaelManaged();

        // It is reasonable to set encryption mode to Cipher Block Chaining
        // (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC;

        // Generate decryptor from the existing key bytes and initialization 
        // vector. Key size will be defined based on the number of the key 
        // bytes.
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                         keyBytes,
                                                         initVectorBytes);

        // Define memory stream which will be used to hold encrypted data.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

        // Define cryptographic stream (always use Read mode for encryption).
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                      decryptor,
                                                      CryptoStreamMode.Read);

        // Since at this point we don't know what the size of decrypted data
        // will be, allocate the buffer long enough to hold ciphertext;
        // plaintext is never longer than ciphertext.
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        // Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                   0,
                                                   plainTextBytes.Length);

        // Close both streams.
        memoryStream.Close();
        cryptoStream.Close();

        // Convert decrypted data into a string. 
        // Let us assume that the original plaintext string was UTF8-encoded.
        string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                   0,
                                                   decryptedByteCount);

        // Return decrypted string.   
        return plainText;
    }

    #endregion
}