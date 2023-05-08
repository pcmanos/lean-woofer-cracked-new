using System;
using System.Runtime.InteropServices;

namespace Lean.Addons
{
	// Token: 0x0200000A RID: 10
	public class DiscordRpc
	{
		// Token: 0x06000096 RID: 150
		[DllImport("runtime.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Discord_Initialize(string applicationId, ref DiscordRpc.EventHandlers handlers, bool autoRegister, string optionalSteamId);

		// Token: 0x06000097 RID: 151
		[DllImport("runtime.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Discord_RunCallbacks();

		// Token: 0x06000098 RID: 152
		[DllImport("runtime.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Discord_Shutdown();

		// Token: 0x06000099 RID: 153
		[DllImport("runtime.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Discord_UpdatePresence(ref DiscordRpc.RichPresence presence);

		// Token: 0x0600009A RID: 154 RVA: 0x00010D3C File Offset: 0x0000EF3C
		internal static void a(string a, ref object b, bool c, object d)
		{
			throw new NotImplementedException();
		}

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x06000113 RID: 275
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void DisconnectedCallback(int errorCode, string message);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x06000117 RID: 279
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ErrorCallback(int errorCode, string message);

		// Token: 0x02000019 RID: 25
		public struct EventHandlers
		{
			// Token: 0x04000095 RID: 149
			public DiscordRpc.ReadyCallback readyCallback;

			// Token: 0x04000096 RID: 150
			public DiscordRpc.DisconnectedCallback disconnectedCallback;

			// Token: 0x04000097 RID: 151
			public DiscordRpc.ErrorCallback errorCallback;
		}

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x0600011B RID: 283
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ReadyCallback();

		// Token: 0x0200001B RID: 27
		[Serializable]
		public struct RichPresence
		{
			// Token: 0x04000098 RID: 152
			public string state;

			// Token: 0x04000099 RID: 153
			public string details;

			// Token: 0x0400009A RID: 154
			public long startTimestamp;

			// Token: 0x0400009B RID: 155
			public long endTimestamp;

			// Token: 0x0400009C RID: 156
			public string largeImageKey;

			// Token: 0x0400009D RID: 157
			public string largeImageText;

			// Token: 0x0400009E RID: 158
			public string smallImageKey;

			// Token: 0x0400009F RID: 159
			public string smallImageText;

			// Token: 0x040000A0 RID: 160
			public string partyId;

			// Token: 0x040000A1 RID: 161
			public int partySize;

			// Token: 0x040000A2 RID: 162
			public int partyMax;

			// Token: 0x040000A3 RID: 163
			public string matchSecret;

			// Token: 0x040000A4 RID: 164
			public string joinSecret;

			// Token: 0x040000A5 RID: 165
			public string spectateSecret;

			// Token: 0x040000A6 RID: 166
			public bool instance;
		}
	}
}
