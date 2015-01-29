using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ImageEncryption
{  
        /// <summary>
        /// 图片加密解密
        /// </summary>
        public class DEncrypt4ImageHelper
        {
            public DEncrypt4ImageHelper() { }
            #region 图片加密方法
            /// <summary>
            /// 加密图片
            /// </summary>
            /// <param name="filePath">源文件</param>
            /// <param name="savePath">目标文件名称</param>
            /// <param name="keyStr">秘钥</param>
            public static void EncryptFile(string filePath, string savePath, string keyStr)
            {
                //使用DES加密
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                FileStream fs = File.OpenRead(filePath);

                byte[] inputByteArray = new byte[fs.Length];

                fs.Read(inputByteArray, 0, (int)fs.Length);

                fs.Close();

                byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);

                SHA1 ha = new SHA1Managed();
                byte[] hb = ha.ComputeHash(keyByteArray);

                byte[] sKey = new byte[8];

                byte[] sIV = new byte[8];
                for (int i = 0; i < 8; i++)
                    sKey[i] = hb[i];
                for (int i = 8; i < 16; i++)
                    sIV[i - 8] = hb[i];

                des.Key = sKey;

                des.IV = sIV;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                fs = File.OpenWrite(savePath);
                foreach (byte b in ms.ToArray())
                {
                    fs.WriteByte(b);
                }
                fs.Close();
                cs.Close();
                ms.Close();
            }

            public static MemoryStream EncryptFile(Stream fileStream, string keyStr)
            {
                //使用DES加密
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);

                byte[] inputByteArray = new byte[fileStream.Length];

                fileStream.Read(inputByteArray, 0, (int)fileStream.Length);

                SHA1 ha = new SHA1Managed();
                byte[] hb = ha.ComputeHash(keyByteArray);

                byte[] sKey = new byte[8];

                byte[] sIV = new byte[8];
                for (int i = 0; i < 8; i++)
                    sKey[i] = hb[i];
                for (int i = 8; i < 16; i++)
                    sIV[i - 8] = hb[i];

                des.Key = sKey;

                des.IV = sIV;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return ms;
            }
            #endregion
            #region Decryption method   image decryption;
            /// <summary>
            /// 图片解密
            /// </summary>
            /// <param name="filePath">目标文件</param>
            /// <param name="savePath">需要转换成的文件</param>
            /// <param name="keyStr">秘钥</param>
            public static void DecryptFile(string filePath, string savePath, string keyStr)
            {
             
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
             
                FileStream fs = File.OpenRead(filePath);
               
                byte[] inputByteArray = new byte[fs.Length];
          
                fs.Read(inputByteArray, 0, (int)fs.Length);
             
                fs.Close();
             
                byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);
            
                SHA1 ha = new SHA1Managed();
             
                byte[] hb = ha.ComputeHash(keyByteArray);
              
                byte[] sKey = new byte[8];
          
                byte[] sIV = new byte[8];
                for (int i = 0; i < 8; i++)
                    sKey[i] = hb[i];
                for (int i = 8; i < 16; i++)
                    sIV[i - 8] = hb[i];
            
                des.Key = sKey;
             
                des.IV = sIV;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                fs = File.OpenWrite(savePath);
                foreach (byte b in ms.ToArray())
                {
                    fs.WriteByte(b);
                }
                fs.Close();
                cs.Close();
                ms.Close();
            }

            public static Stream DecryptFile(string filePath, string keyStr)
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                FileStream fs = File.OpenRead(filePath);


                byte[] inputByteArray = new byte[fs.Length];

                fs.Read(inputByteArray, 0, (int)fs.Length);

                fs.Close();

                byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);

                SHA1 ha = new SHA1Managed();

                byte[] hb = ha.ComputeHash(keyByteArray);

                byte[] sKey = new byte[8];

                byte[] sIV = new byte[8];
                for (int i = 0; i < 8; i++)
                    sKey[i] = hb[i];
                for (int i = 8; i < 16; i++)
                    sIV[i - 8] = hb[i];

                des.Key = sKey;

                des.IV = sIV;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();              
                return ms;
            }
            #endregion

            #region

            #endregion
        }    
}
