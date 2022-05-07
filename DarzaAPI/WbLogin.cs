using System;

namespace DarzaAPI
{
	// Token: 0x0200001D RID: 29
	public class WbLogin : WbPacket
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A75 File Offset: 0x00000C75
		public override WbPacketType Type
		{
			get
			{
				return WbPacketType.Login;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A78 File Offset: 0x00000C78
		public override void Write(ByteWriter w)
		{
			w.WriteString16(this.Email);
			w.WriteString16(this.Password);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A92 File Offset: 0x00000C92
		public override void Read(ByteReader r)
		{
			this.Email = r.ReadString16();
			this.Password = r.ReadString16();
		}

		// Token: 0x04000133 RID: 307
		public string Email;

		// Token: 0x04000134 RID: 308
		public string Password;
	}
}
