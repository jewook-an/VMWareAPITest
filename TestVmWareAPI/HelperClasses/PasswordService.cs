using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class PasswordService {
        private SHA256Managed sha256Managed = new SHA256Managed();
        private RijndaelManaged aes = new RijndaelManaged();

        private readonly string password = @"1443년에 세종대왕이 집현전 학자들의 도움을 얻어 창제(創製)한 우리나라 글자. 자음 17자, 모음 11자 모두 28자로 이루어졌음.";

        public PasswordService() {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
        }


        //AES_256 암호화
        public string AESEncrypt256(string account, string userPassword) {
            var passwordBytes = Encoding.UTF8.GetBytes(userPassword);

            var saltBaseString = $@"ThIs Is {account}'S pAsSwOrD";
            var salt = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(saltBaseString));
            //Console.WriteLine("Salt(Base64) : " + Convert.ToBase64String(salt));


            //PBKDF2(Password-Based Key Derivation Function)
            //반복은 4096번
            var PBKDF2Key = new Rfc2898DeriveBytes(password, salt, 4096);
            var secretKey = PBKDF2Key.GetBytes(aes.KeySize / 8);
            var iv = PBKDF2Key.GetBytes(aes.BlockSize / 8);

            Console.WriteLine("SecretKey(Base64) : " + Convert.ToBase64String(secretKey));
            Console.WriteLine("IV(Base64) : " + Convert.ToBase64String(iv));

            byte[] xBuff = null;
            using (var ms = new MemoryStream()) {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(secretKey, iv), CryptoStreamMode.Write)) {
                    cs.Write(passwordBytes, 0, passwordBytes.Length);
                }
                xBuff = ms.ToArray();
            }
            if (xBuff == null)
                return string.Empty;
            return Convert.ToBase64String(xBuff);
        }

        //AES_256 복호화
        public string AESDecrypt256(string account, string base64EncString) {
            var enccryptedBytes = Convert.FromBase64String(base64EncString);

            var saltBaseString = $@"ThIs Is {account}'S pAsSwOrD";
            var salt = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(saltBaseString));

            //PBKDF2(Password-Based Key Derivation Function)
            var PBKDF2Key = new Rfc2898DeriveBytes(password, salt, 4096);
            var secretKey = PBKDF2Key.GetBytes(aes.KeySize / 8);
            var iv = PBKDF2Key.GetBytes(aes.BlockSize / 8);

            byte[] xBuff = null;
            using (var ms = new MemoryStream()) {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(secretKey, iv), CryptoStreamMode.Write)) {
                    cs.Write(enccryptedBytes, 0, enccryptedBytes.Length);
                }
                xBuff = ms.ToArray();
            }
            if (xBuff == null)
                return string.Empty;
            return Encoding.UTF8.GetString(xBuff);
        }
    }
}