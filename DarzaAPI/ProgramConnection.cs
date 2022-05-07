using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace DarzaAPI
{

	public interface ILoggable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005E RID: 94
		string Name { get; }
	}
	// Token: 0x0200001D RID: 29
	public class ProgramConnection<T> : ILoggable where T : Packet
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003A90 File Offset: 0x00001C90
		public virtual string Name
		{
			get
			{
				return "ProgramConnection";
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003A98 File Offset: 0x00001C98
		public ProgramConnection(string host, int port, int timeout = 5000)
		{
			this.Host = host;
			this.Port = port;
			this.Timeout = timeout;
			this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.SetupSocket(this._socket, timeout);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B1C File Offset: 0x00001D1C
		public ProgramConnection(Socket socket, int timeout = 5000)
		{
			this.Connected = true;
			this.SetupSocket(socket, timeout);
			this._socket = socket;
			IPEndPoint endPoint = (IPEndPoint)socket.RemoteEndPoint;
			this.Host = endPoint.Address.ToString();
			this.Port = endPoint.Port;
			this.Timeout = timeout;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public void StartReceive()
		{
			if (this._socket.Connected)
			{
				this.BeginRead(this._receivePosition, this._receiveSize);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BD5 File Offset: 0x00001DD5
		private void SetupSocket(Socket socket, int timeout)
		{
			socket.NoDelay = true;
			socket.ReceiveTimeout = 5000;
			socket.SendTimeout = 5000;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public void Connect(Action<bool> callback)
		{
			this.Connect(this.Host, this.Port, callback);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C09 File Offset: 0x00001E09
		public void Connect(string host, int port, Action<bool> callback)
		{
			this._socket.BeginConnect(host, port, new AsyncCallback(this.ConnectCallback), callback);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C28 File Offset: 0x00001E28
		private void ConnectCallback(IAsyncResult ar)
		{
			try
			{
				this._socket.EndConnect(ar);
				this.Connected = true;
			}
			catch (Exception e)
			{
				this.Disconnect();
			}
			((Action<bool>)ar.AsyncState)(this.Connected);
			this.StartReceive();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C88 File Offset: 0x00001E88
		public void Disconnect()
		{
			object disconnectLock = this._disconnectLock;
			lock (disconnectLock)
			{
				if (this._disconnected)
				{
					return;
				}
				this._disconnected = true;
			}
			this._socket.Dispose();
			if (this.Connected)
			{
				this.Connected = false;
				Action onDisconnect = this.OnDisconnect;
				if (onDisconnect == null)
				{
					return;
				}
				onDisconnect();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003D00 File Offset: 0x00001F00
		public void Dispose()
		{
			this.Disconnect();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D08 File Offset: 0x00001F08
		private void BeginRead(int offset, int amount)
		{
			if (!this._socket.Connected)
			{
				return;
			}
			try
			{
				this._socket.BeginReceive(this._receiveBuffer, offset, amount, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), null);
			}
			catch (ObjectDisposedException)
			{
				this.Disconnect();
			}
			catch (SocketException)
			{
				this.Disconnect();
			}
			catch (Exception e)
			{
				this.Disconnect();
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D94 File Offset: 0x00001F94
		private void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				int length = this._socket.EndReceive(ar);
				this._receivePosition += length;
				int bytesRemaining = this._receiveSize - this._receivePosition;
				if (length <= 0)
				{
					this.Disconnect();
				}
				else if (bytesRemaining > 0)
				{
					this.BeginRead(this._receivePosition, bytesRemaining);
				}
				else if (!this._receivedSize)
				{
					int size = BitConverter.ToInt32(this._receiveBuffer, 0);
					if (size > this.MaxReceivedSize)
					{
						throw new InvalidOperationException("The received size " + size.ToString() + " is larger than the max size " + this.MaxReceivedSize.ToString());
					}
					this._receiveBuffer = new byte[size];
					this._receiveSize = size;
					this._receivePosition = 0;
					this._receivedSize = true;
					this.BeginRead(this._receivePosition, this._receiveSize);
				}
				else
				{
					byte[] data = new byte[this._receiveSize];
					Array.Copy(this._receiveBuffer, 0, data, 0, this._receiveSize);
					if (this.ParsePacket != null)
					{
						try
						{
							T packet = this.ParsePacket(data);
							this.ReceivedPacket(packet);
						}
						catch (Exception e)
						{
							if (this._socket.Connected)
							{
								this.Disconnect();
							}
							return;
						}
					}
					this._receiveBuffer = new byte[4];
					this._receiveSize = 4;
					this._receivePosition = 0;
					this._receivedSize = false;
					this.BeginRead(this._receivePosition, this._receiveSize);
				}
			}
			catch (ObjectDisposedException)
			{
				this.Disconnect();
			}
			catch (SocketException)
			{
				this.Disconnect();
			}
			catch (Exception e2)
			{
				if (this._socket.Connected)
				{
					this.Disconnect();
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003F98 File Offset: 0x00002198
		protected virtual void ReceivedPacket(T packet)
		{
			Action<T> handlePacket = this.HandlePacket;
			if (handlePacket == null)
			{
				return;
			}
			handlePacket(packet);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003FAC File Offset: 0x000021AC
		public void Send(T packet, Action callback = null)
		{
			packet.State = callback;
			object sendLock = this._sendLock;
			lock (sendLock)
			{
				if (this._sending)
				{
					this._sendQueue.Add(packet);
					return;
				}
				this._sending = true;
			}
			this.SendPacket(packet);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004018 File Offset: 0x00002218
		private void SendPacket(T packet)
		{
			try
			{
				byte[] data = packet.GetData();
				this._socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(this.SendCallback), packet.State);
			}
			catch (ObjectDisposedException)
			{
				this.Disconnect();
			}
			catch (SocketException)
			{
				this.Disconnect();
			}
			catch (Exception e)
			{
				this.Disconnect();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000040A8 File Offset: 0x000022A8
		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				this._socket.EndSend(ar);
				if (ar.IsCompleted)
				{
					Action action = (Action)ar.AsyncState;
					if (action != null)
					{
						action();
					}
				}
				object sendLock = this._sendLock;
				lock (sendLock)
				{
					if (this._sendQueue.Count > 0)
					{
						T packet = this._sendQueue[0];
						this._sendQueue.RemoveAt(0);
						this.SendPacket(packet);
					}
					else
					{
						this._sending = false;
					}
				}
			}
			catch (ObjectDisposedException)
			{
				this.Disconnect();
			}
			catch (SocketException)
			{
				this.Disconnect();
			}
			catch (Exception e)
			{
				this.Disconnect();
			}
		}

		// Token: 0x040000DA RID: 218
		public readonly string Host;

		// Token: 0x040000DB RID: 219
		public readonly int Port;

		// Token: 0x040000DC RID: 220
		public readonly int Timeout;

		// Token: 0x040000DD RID: 221
		public Action OnDisconnect;

		// Token: 0x040000DE RID: 222
		public bool Connected;

		// Token: 0x040000DF RID: 223
		private readonly Socket _socket;

		// Token: 0x040000E0 RID: 224
		private readonly object _disconnectLock = new object();

		// Token: 0x040000E1 RID: 225
		private bool _disconnected;

		// Token: 0x040000E2 RID: 226
		public int MaxReceivedSize = int.MaxValue;

		// Token: 0x040000E3 RID: 227
		private byte[] _receiveBuffer = new byte[4];

		// Token: 0x040000E4 RID: 228
		private int _receivePosition;

		// Token: 0x040000E5 RID: 229
		private int _receiveSize = 4;

		// Token: 0x040000E6 RID: 230
		private bool _receivedSize;

		// Token: 0x040000E7 RID: 231
		public Action<T> HandlePacket;

		// Token: 0x040000E8 RID: 232
		public Func<byte[], T> ParsePacket;

		// Token: 0x040000E9 RID: 233
		private bool _sending;

		// Token: 0x040000EA RID: 234
		private readonly List<T> _sendQueue = new List<T>();

		// Token: 0x040000EB RID: 235
		private readonly object _sendLock = new object();
	}
}
