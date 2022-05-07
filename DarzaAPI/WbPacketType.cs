using System;

namespace DarzaAPI
{
	// Token: 0x02000008 RID: 8
	public enum WbPacketType
	{
		// Token: 0x04000082 RID: 130
		Unknown,
		// Token: 0x04000083 RID: 131
		AssetCheck,
		// Token: 0x04000084 RID: 132
		UpdateAssets,
		// Token: 0x04000085 RID: 133
		AssetCheckResp,
		// Token: 0x04000086 RID: 134
		UpdateAssetsResp,
		// Token: 0x04000087 RID: 135
		Auth,
		// Token: 0x04000088 RID: 136
		AuthResp,
		// Token: 0x04000089 RID: 137
		Login,
		// Token: 0x0400008A RID: 138
		LoginResp,
		// Token: 0x0400008B RID: 139
		Link,
		// Token: 0x0400008C RID: 140
		LinkResp,
		// Token: 0x0400008D RID: 141
		Forgot,
		// Token: 0x0400008E RID: 142
		ForgotResp,
		// Token: 0x0400008F RID: 143
		Register,
		// Token: 0x04000090 RID: 144
		RegisterResp,
		// Token: 0x04000091 RID: 145
		Change,
		// Token: 0x04000092 RID: 146
		ChangeResp,
		// Token: 0x04000093 RID: 147
		ServerList,
		// Token: 0x04000094 RID: 148
		ServerListResp,
		// Token: 0x04000095 RID: 149
		Reverify,
		// Token: 0x04000096 RID: 150
		ReverifyResp,
		// Token: 0x04000097 RID: 151
		PurchaseResp,
		// Token: 0x04000098 RID: 152
		PurchaseSteam,
		// Token: 0x04000099 RID: 153
		PurchaseVerifySteam,
		// Token: 0x0400009A RID: 154
		GetProducts,
		// Token: 0x0400009B RID: 155
		ProductsResp,
		// Token: 0x0400009C RID: 156
		PurchaseVerifyDiscord,
		// Token: 0x0400009D RID: 157
		CrashReport,
		// Token: 0x0400009E RID: 158
		CrashReportResponse,
		// Token: 0x0400009F RID: 159
		BuyPackage,
		// Token: 0x040000A0 RID: 160
		BuyPackageAck,
		// Token: 0x040000A1 RID: 161
		WbVerifyDroidPurchase,
		// Token: 0x040000A2 RID: 162
		WbVerifyDroidPurchaseAck,
		// Token: 0x040000A3 RID: 163
		Failure
	}
}
