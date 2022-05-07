using System;

namespace DarzaAPI
{
	// Token: 0x02000025 RID: 37
	public class WbRegister : WbPacket
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public override WbPacketType Type
		{
			get
			{
				return WbPacketType.Register;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public override void Write(ByteWriter w)
		{
			w.Write(this.HasEmail);
			if (this.HasEmail)
			{
				w.WriteString16(this.Email);
				w.WriteString16(this.Password);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002E06 File Offset: 0x00001006
		public override void Read(ByteReader r)
		{
			this.HasEmail = r.ReadBool();
			if (this.HasEmail)
			{
				this.Email = r.ReadString16();
				this.Password = r.ReadString16();
			}
		}

		// Token: 0x04000143 RID: 323
		public bool HasEmail;

		// Token: 0x04000144 RID: 324
		public string Email;

		// Token: 0x04000145 RID: 325
		public string Password;
	}
}
