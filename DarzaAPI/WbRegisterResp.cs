using System;

namespace DarzaAPI
{
	// Token: 0x02000026 RID: 38
	public class WbRegisterResp : WbPacket
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002E3C File Offset: 0x0000103C
		public override WbPacketType Type
		{
			get
			{
				return WbPacketType.RegisterResp;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E40 File Offset: 0x00001040
		public override void Write(ByteWriter w)
		{
			w.Write(this.Success);
			w.WriteString8(this.Value);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002E5A File Offset: 0x0000105A
		public override void Read(ByteReader r)
		{
			this.Success = r.ReadBool();
			this.Value = r.ReadString8();
		}

		// Token: 0x04000146 RID: 326
		public bool Success;

		// Token: 0x04000147 RID: 327
		public string Value;
	}
}
