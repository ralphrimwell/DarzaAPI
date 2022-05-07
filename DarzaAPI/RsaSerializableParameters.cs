using System;
using System.Security.Cryptography;

namespace DarzaAPI
{
	// Token: 0x02000126 RID: 294
	public class RsaSerializableParameters
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x000553C7 File Offset: 0x000535C7
		public RsaSerializableParameters()
		{
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000553CF File Offset: 0x000535CF
		public RsaSerializableParameters(RSAParameters parameters)
		{
			this.ImportParameters(parameters);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000553E0 File Offset: 0x000535E0
		public void ImportParameters(RSAParameters parameters)
		{
			this.D = this.StringValue(parameters.D);
			this.DP = this.StringValue(parameters.DP);
			this.DQ = this.StringValue(parameters.DQ);
			this.Exponent = this.StringValue(parameters.Exponent);
			this.InverseQ = this.StringValue(parameters.InverseQ);
			this.Modulus = this.StringValue(parameters.Modulus);
			this.P = this.StringValue(parameters.P);
			this.Q = this.StringValue(parameters.Q);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00055480 File Offset: 0x00053680
		public RSAParameters GetParameters()
		{
			return new RSAParameters
			{
				D = this.ByteValue(this.D),
				DP = this.ByteValue(this.DP),
				DQ = this.ByteValue(this.DQ),
				Exponent = this.ByteValue(this.Exponent),
				InverseQ = this.ByteValue(this.InverseQ),
				Modulus = this.ByteValue(this.Modulus),
				P = this.ByteValue(this.P),
				Q = this.ByteValue(this.Q)
			};
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0005552E File Offset: 0x0005372E
		private string StringValue(byte[] bytes)
		{
			if (bytes != null)
			{
				return Convert.ToBase64String(bytes);
			}
			return null;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0005553B File Offset: 0x0005373B
		private byte[] ByteValue(string base64Value)
		{
			if (base64Value != null)
			{
				return Convert.FromBase64String(base64Value);
			}
			return null;
		}

		// Token: 0x04000943 RID: 2371
		public string D;

		// Token: 0x04000944 RID: 2372
		public string DP;

		// Token: 0x04000945 RID: 2373
		public string DQ;

		// Token: 0x04000946 RID: 2374
		public string Exponent;

		// Token: 0x04000947 RID: 2375
		public string InverseQ;

		// Token: 0x04000948 RID: 2376
		public string Modulus;

		// Token: 0x04000949 RID: 2377
		public string P;

		// Token: 0x0400094A RID: 2378
		public string Q;
	}
}
