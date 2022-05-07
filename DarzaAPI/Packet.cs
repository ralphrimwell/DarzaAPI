using System;

namespace DarzaAPI
{
	// Token: 0x0200001B RID: 27
	public abstract class Packet
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003A7D File Offset: 0x00001C7D
		public virtual byte Id
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600009F RID: 159
		public abstract byte[] GetData();

		// Token: 0x060000A0 RID: 160
		public abstract void Write(ByteWriter w);

		// Token: 0x060000A1 RID: 161
		public abstract void Read(ByteReader r);

		// Token: 0x040000D6 RID: 214
		public object State;
	}
}
