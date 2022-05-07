using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace DarzaAPI
{
    public class RSA
    {
		public byte[] EncryptRsa(byte[] data)
		{
			RsaSerializableParameters PublicKey = JsonConvert.DeserializeObject<RsaSerializableParameters>("{\"D\":null,\"DP\":null,\"DQ\":null,\"Exponent\":\"AQAB\",\"InverseQ\":null,\"Modulus\":\"8+C29kcJKLzf+Sm7Tfm5wIuMvkYb2Yh+2hvuEKEchxxzvMJpwSfC2GREUWPW6Mn48StK2odUcKc47oosfpnVDjzHwr7Jju+QQvKPoh1Q+z4zrP1ac75S+fL2VFAX8W8TbmYAfKn0LR5YbELjmYeW8537f8zlJL59ivNgynXtHrU=\",\"P\":null,\"Q\":null}");

			RSACryptoServiceProvider RSA2 = new RSACryptoServiceProvider();
			RSA2.ImportParameters(PublicKey.GetParameters());

			return RSA2.Encrypt(data, RSAEncryptionPadding.Pkcs1);
		}
		public string EncryptRsaBase64(string value)
		{
			return this.EncryptRsaBase64(value, Encoding.UTF8);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00055194 File Offset: 0x00053394
		public string EncryptRsaBase64(string value, Encoding encoding)
		{
			byte[] bytes = encoding.GetBytes(value);
			return Convert.ToBase64String(this.EncryptRsa(bytes));
		}
	}
}
