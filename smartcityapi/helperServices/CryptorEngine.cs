using System.Security.Cryptography;
using System.Text;

namespace smartcityapi.helperServices
{
	public class CryptorEngine
	{  //private readonly IConfiguration _configuration;
	   //static string _KEY;

		//public CryptorEngine(IConfiguration configuration)
		//{
		//    _configuration = configuration;
		//    _KEY = _configuration["Jwt:Key"].ToString();
		//}

		//static string _KEY = "dmsRajkot";
		//static string _KEY = "dmsDelhi";
		static string _KEY = "myVTSServiceAccessToken@2024";

		/// <summary>
		/// Encrypt a string using dual encryption method. Return a encrypted cipher Text
		/// </summary>
		/// <param name="toEncrypt">string to be encrypted</param>
		/// <param name="useHashing">use hashing? send to for extra secirity</param>
		/// <returns></returns>

		public static string Encrypt(string toEncrypt, bool useHashing)
		{
			byte[] buffer;
			byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
			//AppSettingsReader reader = new AppSettingsReader();
			if (!useHashing)
			{
				buffer = Encoding.UTF8.GetBytes(_KEY);
			}
			else
			{
				MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
				buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(_KEY));
				provider.Clear();
			}
			TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
			{
				Key = buffer,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};
			byte[] inArray = provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
			provider2.Clear();
			return Convert.ToBase64String(inArray, 0, inArray.Length);
		}


		/// <summary>
		/// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
		/// </summary>
		/// <param name="cipherString">encrypted string</param>
		/// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
		/// <returns></returns>
		public static string Decrypt(string cipherString, bool useHashing)
		{
			byte[] buffer;
			byte[] inputBuffer = Convert.FromBase64String(cipherString);
			//AppSettingsReader reader = new AppSettingsReader();
			if (!useHashing)
			{
				buffer = Encoding.UTF8.GetBytes(_KEY);
			}
			else
			{
				MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
				buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(_KEY));
				provider.Clear();
			}
			TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
			{
				Key = buffer,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};
			byte[] bytes = provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
			provider2.Clear();
			return Encoding.UTF8.GetString(bytes);
		}

	}
}
