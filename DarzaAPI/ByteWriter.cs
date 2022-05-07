using System;
using System.IO;
using System.Text;

namespace DarzaAPI
{
	// Token: 0x02000005 RID: 5
	public class ByteWriter
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000021FD File Offset: 0x000003FD
		public ByteWriter(Stream stream)
		{
			this._writer = new BinaryWriter(stream);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002211 File Offset: 0x00000411
		public void Dispose()
		{
			this._writer.Dispose();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000221E File Offset: 0x0000041E
		public void Write(double value)
		{
			this._writer.Write(value);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000222C File Offset: 0x0000042C
		public void Write(float value)
		{
			this._writer.Write(value);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000223A File Offset: 0x0000043A
		public void Write(ulong value)
		{
			this._writer.Write(value);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002248 File Offset: 0x00000448
		public void Write(long value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002256 File Offset: 0x00000456
		public void Write(uint value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002264 File Offset: 0x00000464
		public void Write(int value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002272 File Offset: 0x00000472
		public void Write(ushort value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002280 File Offset: 0x00000480
		public void Write(short value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000228E File Offset: 0x0000048E
		public void Write(byte value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000229C File Offset: 0x0000049C
		public void Write(sbyte value)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000022AC File Offset: 0x000004AC
		public void WriteString8(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			this._writer.Write((byte)bytes.Length);
			this._writer.Write(bytes);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022E0 File Offset: 0x000004E0
		public void WriteString16(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			this._writer.Write((ushort)bytes.Length);
			this._writer.Write(bytes);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002314 File Offset: 0x00000514
		public void WriteString32(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			this._writer.Write((uint)bytes.Length);
			this._writer.Write(bytes);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002347 File Offset: 0x00000547
		public void Write(bool value)
		{
			this._writer.Write(value ? 1 : 0);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000235C File Offset: 0x0000055C
		public void Write(byte[] value)
		{
			this._writer.Write(value);
		}



		// Token: 0x0600002D RID: 45 RVA: 0x0000239E File Offset: 0x0000059E
		public void Write(DateTime time)
		{
			this.Write(time.Ticks);
		}

		// Token: 0x0400000A RID: 10
		private readonly BinaryWriter _writer;
	}
}
