using System;
using System.Collections.Generic;
using System.Text;

namespace EyouSoft.Common.EncryptUtility
{
    /// <summary>
    /// 加密解密 数据库连接字符串
    /// </summary>
    public class ConnectionInfo
    {
        private static string _Key = "12$#@!#@5trfdewsfr54321234edw%$#";
        private static string _IV = "!54~14874&%@+-)#";
       
        #region HashCrypto 加密
        /// <summary>
        /// 加密数据库连接
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        public static string EncryDbConnStr(string Values)
        {
            string tmpVal = "";
            HashCrypto crypto = new HashCrypto();
            crypto.Key = _Key;
            crypto.IV = _IV;
            tmpVal = crypto.RijndaelEncrypt(Values);
            crypto = null;
            return tmpVal;
        }
        /// <summary>
        /// 解密数据库连接
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        public static string DecryptDbConnStr(string Values)
        {
            string tmpVal = "";
            HashCrypto crypto = new HashCrypto();
            crypto.Key = _Key;
            crypto.IV = _IV;
            tmpVal = crypto.DeRijndaelEncrypt(Values);
            crypto = null;
            return tmpVal;
        }
        #endregion

        #region Base 64
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputConnectionString"></param>
        /// <returns></returns>
        public static string DecryptDBConnectionString(string InputConnectionString)
        {
            // If the variable is blank, return the input
            if (InputConnectionString.Equals(string.Empty))
            {
                return InputConnectionString;
            }

            // Create an instance of the encryption API
            // We assume the key has been encrypted on this machine and not by a user
            DataProtector dp = new DataProtector(Store.Machine);

            // Use the API to decrypt the connection string
            // API works with bytes so we need to convert to and from byte arrays
            try
            {
                byte[] decryptedData = dp.Decrypt(Convert.FromBase64String(InputConnectionString), null);
                // Return the decyrpted data to the string
                return Encoding.ASCII.GetString(decryptedData);
            }
            catch (Exception ex)
            {
                throw new Exception("UnDecrypt the Connection string fail or Connection to sql server error!. " + ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string EncryptDBConnectionString(string encryptedString)
        {

            // Create an instance of the encryption API
            // We assume the key has been encrypted on this machine and not by a user
            DataProtector dp = new DataProtector(Store.Machine);

            // Use the API to encrypt the connection string
            // API works with bytes so we need to convert to and from byte arrays
            byte[] dataBytes = Encoding.ASCII.GetBytes(encryptedString);
            byte[] encryptedBytes = dp.Encrypt(dataBytes, null);

            // Return the encyrpted data to the string
            return Convert.ToBase64String(encryptedBytes);
        }
        #endregion
    }
}
