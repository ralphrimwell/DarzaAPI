using System;

namespace DarzaAPI
{
	// Token: 0x0200001E RID: 30
	public class WbLoginResp : WbPacket
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public override WbPacketType Type
		{
			get
			{
				return WbPacketType.LoginResp;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AB7 File Offset: 0x00000CB7
		public override void Write(ByteWriter w)
		{
			w.Write(this.Success);
			w.WriteString8(this.Value);
			if (this.Success)
			{
				w.WriteString8(this.Username);
				w.Write(this.EmailVerified);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public override void Read(ByteReader r)
		{
			this.Success = r.ReadBool();
			this.Value = r.ReadString8();
			if (this.Success)
			{
				this.Username = r.ReadString8();
				this.EmailVerified = r.ReadBool();
			}
		}

		// Token: 0x04000135 RID: 309
		public bool Success;

		// Token: 0x04000136 RID: 310
		public string Value;

		// Token: 0x04000137 RID: 311
		public string Username;

		// Token: 0x04000138 RID: 312
		public bool EmailVerified;
	}
}
