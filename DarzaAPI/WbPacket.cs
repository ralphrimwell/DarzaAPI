using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


namespace DarzaAPI
{
	// Token: 0x0200001F RID: 31
	public abstract class WbPacket : Packet
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B33 File Offset: 0x00000D33
		public override byte Id
		{
			get
			{
				return (byte)this.Type;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000055 RID: 85
		public abstract WbPacketType Type { get; }

		// Token: 0x06000056 RID: 86 RVA: 0x00002B3C File Offset: 0x00000D3C
		public override byte[] GetData()
		{
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				ByteWriter w = new ByteWriter(stream);
				w.Write(0);
				w.Write(this.Id);
				this.Write(w);
				byte[] array = stream.ToArray();
				byte[] sizeBytes = BitConverter.GetBytes(array.Length - 4);
				array[0] = sizeBytes[0];
				array[1] = sizeBytes[1];
				array[2] = sizeBytes[2];
				array[3] = sizeBytes[3];
				result = array;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public static WbPacket Parse(byte[] data)
		{
			WbPacket result;
			using (MemoryStream stream = new MemoryStream(data))
			{
				ByteReader r = new ByteReader(stream);
				byte id = r.ReadByte();
				WbPacket wbPacket = (WbPacket)Activator.CreateInstance(WbPacket._packetTypes.Value[id]);
				wbPacket.Read(r);
				r.Dispose();
				result = wbPacket;
			}
			return result;
		}

		// Token: 0x04000139 RID: 313
		private static readonly ThreadLocal<Dictionary<byte, Type>> _packetTypes = new ThreadLocal<Dictionary<byte, Type>>(delegate ()
		{
			Type type = typeof(WbPacket);
			return (from _ in type.Assembly.GetTypes()
					where _.IsSubclassOf(type)
					select _).ToDictionary((Type _) => ((WbPacket)Activator.CreateInstance(_)).Id);
		});
	}
}
