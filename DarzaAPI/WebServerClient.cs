using System;
using System.Collections.Generic;


namespace DarzaAPI
{
	// Token: 0x02000065 RID: 101
	public class WebServerClient
	{




		// Token: 0x06000372 RID: 882 RVA: 0x0001B038 File Offset: 0x00019238
		

		// Token: 0x06000373 RID: 883 RVA: 0x0001B0A0 File Offset: 0x000192A0


		// Token: 0x06000374 RID: 884 RVA: 0x0001B0F0 File Offset: 0x000192F0


		// Token: 0x06000375 RID: 885 RVA: 0x0001B140 File Offset: 0x00019340


		// Token: 0x06000376 RID: 886 RVA: 0x0001B198 File Offset: 0x00019398
		public static void Register(string email, string password, Action<bool, string> onCompleteAction)
		{
			WebServerClient client = null;
			RSA enc = new RSA();
			client = new WebServerClient("13.52.121.100", new WbRegister
			{
				HasEmail = true,
				Email = enc.EncryptRsaBase64(email),
				Password = enc.EncryptRsaBase64(password)
			}, delegate (WbPacket packet)
			{
				if (!(packet is WbRegisterResp))
				{
					onCompleteAction(false, null);
					client.Dispose();
					return;
				}
				WbRegisterResp resp = (WbRegisterResp)packet;
				onCompleteAction(resp.Success, resp.Value);
			}, 5000);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001B208 File Offset: 0x00019408


		// Token: 0x06000378 RID: 888 RVA: 0x0001B278 File Offset: 0x00019478
		public static void Login(string email, string password, Action<WbLoginResp> onCompleteAction)
		{
			RSA enc = new RSA();
			WebServerClient client = null;
			client = new WebServerClient("13.52.121.100", new WbLogin
			{
				Email = enc.EncryptRsaBase64(email),
				Password = enc.EncryptRsaBase64(password)
			}, delegate (WbPacket packet)
			{
				if (!(packet is WbLoginResp))
				{
					onCompleteAction(null);
					client.Dispose();
					return;
				}
				WbLoginResp resp = (WbLoginResp)packet;
				onCompleteAction(resp);
				client.Dispose();
			}, 5000);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001B2E4 File Offset: 0x000194E4


		// Token: 0x0600037A RID: 890 RVA: 0x0001B33C File Offset: 0x0001953C


		// Token: 0x0600037B RID: 891 RVA: 0x0001B384 File Offset: 0x00019584


		// Token: 0x0600037C RID: 892 RVA: 0x0001B3E0 File Offset: 0x000195E0
		public WebServerClient(string host, WbPacket packet, Action<WbPacket> packetHandler, int timeout = 5000)
		{
			this._packet = packet;
			this._packetHandler = packetHandler;
			this._connection = new ProgramConnection<WbPacket>(host, 6406, timeout)
			{	
				ParsePacket = new Func<byte[], WbPacket>(WbPacket.Parse),
				HandlePacket = new Action<WbPacket>(this.HandlePacket),
				OnDisconnect = new Action(this.DisconnectCallback)
			};
			this._connection.Connect(new Action<bool>(this.ConnectionCallback));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001B461 File Offset: 0x00019661
		public void Send(WbPacket packet, Action<WbPacket> packetHandler)
		{
			this._packetHandler = packetHandler;
			this._connection.Send(packet, null);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001B477 File Offset: 0x00019677
		private void HandlePacket(WbPacket packet)
		{
			Action<WbPacket> packetHandler = this._packetHandler;
			this._packetHandler = null;
			if (packetHandler == null)
			{
				return;
			}
			packetHandler(packet);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001B491 File Offset: 0x00019691
		private void ConnectionCallback(bool connected)
		{
			if (connected)
			{
				this._connection.Send(this._packet, null);
				this._packet = null;
				return;
			}
			Action<WbPacket> packetHandler = this._packetHandler;
			if (packetHandler == null)
			{
				return;
			}
			packetHandler(null);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001B4C1 File Offset: 0x000196C1
		private void DisconnectCallback()
		{
			Action<WbPacket> packetHandler = this._packetHandler;
			if (packetHandler == null)
			{
				return;
			}
			packetHandler(null);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001B4D4 File Offset: 0x000196D4
		public void Dispose()
		{
			this._connection.Dispose();
		}

		// Token: 0x0400037E RID: 894
		public const string Web_Server_Host = "13.52.121.100";

		// Token: 0x0400037F RID: 895

		// Token: 0x04000380 RID: 896
		public static bool OfflineMode;

		// Token: 0x04000381 RID: 897
		private readonly ProgramConnection<WbPacket> _connection;

		// Token: 0x04000382 RID: 898
		private Action<WbPacket> _packetHandler;

		// Token: 0x04000383 RID: 899
		private WbPacket _packet;
	}
}
