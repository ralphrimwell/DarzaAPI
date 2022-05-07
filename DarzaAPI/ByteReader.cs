using System;
using System.IO;
using System.Text;

namespace DarzaAPI
{
	// Token: 0x02000004 RID: 4
	public class ByteReader
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020A3 File Offset: 0x000002A3
		public ByteReader(Stream stream)
		{
			this._reader = new BinaryReader(stream);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B7 File Offset: 0x000002B7
		public void Dispose()
		{
			this._reader.Dispose();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C4 File Offset: 0x000002C4
		public double ReadDouble()
		{
			return this._reader.ReadDouble();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020D1 File Offset: 0x000002D1
		public float ReadFloat()
		{
			return this._reader.ReadSingle();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020DE File Offset: 0x000002DE
		public ulong ReadUInt64()
		{
			return this._reader.ReadUInt64();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020EB File Offset: 0x000002EB
		public long ReadInt64()
		{
			return this._reader.ReadInt64();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020F8 File Offset: 0x000002F8
		public uint ReadUInt32()
		{
			return this._reader.ReadUInt32();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002105 File Offset: 0x00000305
		public int ReadInt32()
		{
			return this._reader.ReadInt32();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002112 File Offset: 0x00000312
		public ushort ReadUInt16()
		{
			return this._reader.ReadUInt16();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000211F File Offset: 0x0000031F
		public short ReadInt16()
		{
			return this._reader.ReadInt16();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000212C File Offset: 0x0000032C
		public byte ReadByte()
		{
			return this._reader.ReadByte();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002139 File Offset: 0x00000339
		public sbyte ReadSByte()
		{
			return this._reader.ReadSByte();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002146 File Offset: 0x00000346
		public string ReadString8()
		{
			return Encoding.UTF8.GetString(this._reader.ReadBytes((int)this._reader.ReadByte()));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002168 File Offset: 0x00000368
		public string ReadString16()
		{
			return Encoding.UTF8.GetString(this._reader.ReadBytes((int)this._reader.ReadUInt16()));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000218A File Offset: 0x0000038A
		public string ReadString32()
		{
			return Encoding.UTF8.GetString(this._reader.ReadBytes(this._reader.ReadInt32()));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021AC File Offset: 0x000003AC
		public bool ReadBool()
		{
			return this._reader.ReadByte() == 1;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021BC File Offset: 0x000003BC
		public byte[] ReadBytes(int amount)
		{
			return this._reader.ReadBytes(amount);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021CA File Offset: 0x000003CA


		// Token: 0x06000019 RID: 25 RVA: 0x000021F0 File Offset: 0x000003F0
		public DateTime ReadDateTime()
		{
			return new DateTime(this.ReadInt64());
		}

		// Token: 0x04000009 RID: 9
		private readonly BinaryReader _reader;
	}
}
