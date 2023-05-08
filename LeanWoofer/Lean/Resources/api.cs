using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace Lean.Resources
{
	// Token: 0x02000006 RID: 6
	public class api
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000246D File Offset: 0x0000066D
		public static bool IsDebugRelease
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002470 File Offset: 0x00000670
		public api(string name, string ownerid, string secret, string version)
		{
			bool flag = string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ownerid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version);
			if (flag)
			{
				api.error("Security error, please reinstall the software!");
				Environment.Exit(0);
			}
			this.name = name;
			this.ownerid = ownerid;
			this.secret = secret;
			this.version = version;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002510 File Offset: 0x00000710
		public void init()
		{
			this.m_b = encryption.sha256(encryption.iv_key());
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("init"));
			nameValueCollection["ver"] = encryption.encrypt(this.version, this.secret, text);
			nameValueCollection["hash"] = api.checksum(Process.GetCurrentProcess().MainModule.FileName);
			nameValueCollection["enckey"] = encryption.encrypt(this.m_b, this.secret, text);
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			bool flag = text2 == "KeyAuth_Invalid";
			if (flag)
			{
				MessageBox.Show("Missing dll files, extract whole folder", "Lean Woofer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Application.Exit();
			}
			text2 = encryption.decrypt(text2, this.secret, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.b(response_structure.appinfo);
				this.m_a = response_structure.sessionid;
				this.m_c = true;
			}
			else
			{
				bool flag2 = response_structure.message == "invalidver";
				if (flag2)
				{
					this.app_data.downloadLink = response_structure.download;
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000026BC File Offset: 0x000008BC
		public void Checkinit()
		{
			DateTime t = new DateTime(2022, 7, 9);
			bool flag = t < DateTime.Now;
			if (flag)
			{
				throw new ArgumentException();
			}
			bool flag2 = !this.m_c;
			if (flag2)
			{
				bool isDebugRelease = api.IsDebugRelease;
				if (isDebugRelease)
				{
					api.error("Security error, please reinstall the software! (2)");
				}
				else
				{
					api.error("Security error, please reinstall the software! (2)");
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002724 File Offset: 0x00000924
		public void register(string username, string pass, string key)
		{
			DateTime t = new DateTime(2022, 7, 9, 2, 22, 41);
			bool flag = !(DateTime.Now > t);
			if (flag)
			{
			}
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("register"));
			nameValueCollection["username"] = encryption.encrypt(username, this.m_b, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.m_b, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.m_b, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.c(response_structure.info);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000028C8 File Offset: 0x00000AC8
		public void login(string username, string pass)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("login"));
			nameValueCollection["username"] = encryption.encrypt(username, this.m_b, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.m_b, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.c(response_structure.info);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002A24 File Offset: 0x00000C24
		public void upgrade(string username, string key)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("upgrade"));
			nameValueCollection["username"] = encryption.encrypt(username, this.m_b, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			response_structure.success = false;
			this.d(response_structure);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002B50 File Offset: 0x00000D50
		public void license(string key)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("license"));
			nameValueCollection["key"] = encryption.encrypt(key, this.m_b, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.c(response_structure.info);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002C94 File Offset: 0x00000E94
		public void check()
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("check"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure a2 = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(a2);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002D74 File Offset: 0x00000F74
		public void setvar(string var, string data)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("setvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.m_b, text);
			nameValueCollection["data"] = encryption.encrypt(data, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure a2 = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(a2);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002E88 File Offset: 0x00001088
		public string getvar(string var)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("getvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			string result;
			if (success)
			{
				result = response_structure.response;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void ban()
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("ban"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure a2 = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(a2);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003080 File Offset: 0x00001280
		public string var(string varid)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("var"));
			nameValueCollection["varid"] = encryption.encrypt(varid, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			string result;
			if (success)
			{
				result = response_structure.message;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000031A8 File Offset: 0x000013A8
		public List<api.msg> chatget(string channelname)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatget"));
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			List<api.msg> result;
			if (success)
			{
				result = response_structure.messages;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000032C0 File Offset: 0x000014C0
		public bool chatsend(string msg, string channelname)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatsend"));
			nameValueCollection["message"] = encryption.encrypt(msg, this.m_b, text);
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			return response_structure.success;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000033EC File Offset: 0x000015EC
		public bool checkblack()
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("checkblacklist"));
			nameValueCollection["hwid"] = encryption.encrypt(value, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			return response_structure.success;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003514 File Offset: 0x00001714
		public string webhook(string webid, string param, string body = "", string conttype = "")
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("webhook"));
			nameValueCollection["webid"] = encryption.encrypt(webid, this.m_b, text);
			nameValueCollection["params"] = encryption.encrypt(param, this.m_b, text);
			nameValueCollection["body"] = encryption.encrypt(body, this.m_b, text);
			nameValueCollection["conttype"] = encryption.encrypt(conttype, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			string result;
			if (success)
			{
				result = response_structure.response;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003678 File Offset: 0x00001878
		public byte[] download(string fileid)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("file"));
			nameValueCollection["fileid"] = encryption.encrypt(fileid, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			string text2 = api.a(a);
			text2 = encryption.decrypt(text2, this.m_b, text);
			api.response_structure response_structure = this.m_d.string_to_generic<api.response_structure>(text2);
			this.d(response_structure);
			bool success = response_structure.success;
			byte[] result;
			if (success)
			{
				result = encryption.str_to_byte_arr(response_structure.contents);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003794 File Offset: 0x00001994
		public void log(string message)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("log"));
			nameValueCollection["pcuser"] = encryption.encrypt(Environment.UserName, this.m_b, text);
			nameValueCollection["message"] = encryption.encrypt(message, this.m_b, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.m_a));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection a = nameValueCollection;
			api.a(a);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003888 File Offset: 0x00001A88
		public static string checksum(string filename)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					byte[] value = md.ComputeHash(fileStream);
					result = BitConverter.ToString(value).Replace("-", "").ToLowerInvariant();
				}
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000038FC File Offset: 0x00001AFC
		public static void error(string message)
		{
			MessageBox.Show(message, "Lean Protection", MessageBoxButtons.OK);
			Environment.Exit(0);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003914 File Offset: 0x00001B14
		private static string a(NameValueCollection a)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.0/", a);
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				HttpStatusCode statusCode = httpWebResponse.StatusCode;
				HttpStatusCode httpStatusCode = statusCode;
				bool flag = httpStatusCode != (HttpStatusCode)429;
				if (flag)
				{
					api.error("Connection failure, Try again!");
					Environment.Exit(0);
					result = "";
				}
				else
				{
					api.error("You are connecting too fast to Woofer, please wait!");
					Environment.Exit(0);
					result = "";
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000039D0 File Offset: 0x00001BD0
		private void b(api.app_data_structure a)
		{
			this.app_data.numUsers = a.numUsers;
			this.app_data.numOnlineUsers = a.numOnlineUsers;
			this.app_data.numKeys = a.numKeys;
			this.app_data.version = a.version;
			this.app_data.customerPanelLink = a.customerPanelLink;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003A38 File Offset: 0x00001C38
		private void c(api.user_data_structure a)
		{
			this.user_data.username = a.username;
			this.user_data.ip = a.ip;
			this.user_data.hwid = a.hwid;
			this.user_data.createdate = a.createdate;
			this.user_data.lastlogin = a.lastlogin;
			this.user_data.subscriptions = a.subscriptions;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public string expirydaysleft()
		{
			this.Checkinit();
			DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
			TimeSpan timeSpan = d - DateTime.Now;
			return Convert.ToString(timeSpan.Days.ToString() + " Days " + timeSpan.Hours.ToString() + " Hours Left");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003B1E File Offset: 0x00001D1E
		private void d(api.response_structure a)
		{
			this.response.success = a.success;
			this.response.message = a.message;
		}

		// Token: 0x04000006 RID: 6
		public string name;

		// Token: 0x04000007 RID: 7
		public string ownerid;

		// Token: 0x04000008 RID: 8
		public string secret;

		// Token: 0x04000009 RID: 9
		public string version;

		// Token: 0x0400000A RID: 10
		private string m_a;

		// Token: 0x0400000B RID: 11
		private string m_b;

		// Token: 0x0400000C RID: 12
		private bool m_c;

		// Token: 0x0400000D RID: 13
		public api.app_data_class app_data = new api.app_data_class();

		// Token: 0x0400000E RID: 14
		public api.user_data_class user_data = new api.user_data_class();

		// Token: 0x0400000F RID: 15
		public api.response_class response = new api.response_class();

		// Token: 0x04000010 RID: 16
		private json_wrapper m_d = new json_wrapper(new api.response_structure());

		// Token: 0x0200000D RID: 13
		[DataContract]
		private class response_structure
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x0001115C File Offset: 0x0000F35C
			// (set) Token: 0x060000B4 RID: 180 RVA: 0x00011174 File Offset: 0x0000F374
			[DataMember]
			public bool success { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x00011180 File Offset: 0x0000F380
			// (set) Token: 0x060000B6 RID: 182 RVA: 0x00011198 File Offset: 0x0000F398
			[DataMember]
			public string sessionid
			{
				[CompilerGenerated]
				get
				{
					return this.b;
				}
				[CompilerGenerated]
				set
				{
					DateTime dateTime = default(DateTime);
					dateTime = dateTime.AddYears(2021);
					dateTime = dateTime.AddMonths(6);
					DateTime t = dateTime.AddDays(7.8650462962963);
					bool flag = !(DateTime.Now > t);
					if (flag)
					{
					}
					this.b = value;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x000111F4 File Offset: 0x0000F3F4
			// (set) Token: 0x060000B8 RID: 184 RVA: 0x00011251 File Offset: 0x0000F451
			[DataMember]
			public string contents
			{
				[CompilerGenerated]
				get
				{
					DateTime dateTime = default(DateTime);
					dateTime = dateTime.AddYears(2021);
					dateTime = dateTime.AddMonths(6);
					DateTime t = dateTime.AddDays(7.58689814814815);
					bool flag = !(t < DateTime.Now);
					if (flag)
					{
					}
					return this.c;
				}
				[CompilerGenerated]
				set
				{
					this.c = value;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x0001125C File Offset: 0x0000F45C
			// (set) Token: 0x060000BA RID: 186 RVA: 0x00011274 File Offset: 0x0000F474
			[DataMember]
			public string response { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000BB RID: 187 RVA: 0x00011280 File Offset: 0x0000F480
			// (set) Token: 0x060000BC RID: 188 RVA: 0x00011298 File Offset: 0x0000F498
			[DataMember]
			public string message { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000BD RID: 189 RVA: 0x000112A4 File Offset: 0x0000F4A4
			// (set) Token: 0x060000BE RID: 190 RVA: 0x000112BC File Offset: 0x0000F4BC
			[DataMember]
			public string download { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000BF RID: 191 RVA: 0x000112C8 File Offset: 0x0000F4C8
			// (set) Token: 0x060000C0 RID: 192 RVA: 0x000112E0 File Offset: 0x0000F4E0
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.user_data_structure info { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x000112EC File Offset: 0x0000F4EC
			// (set) Token: 0x060000C2 RID: 194 RVA: 0x00011304 File Offset: 0x0000F504
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.app_data_structure appinfo { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x00011310 File Offset: 0x0000F510
			// (set) Token: 0x060000C4 RID: 196 RVA: 0x00011328 File Offset: 0x0000F528
			[DataMember]
			public List<api.msg> messages { get; set; }

			// Token: 0x04000066 RID: 102
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool a;

			// Token: 0x04000067 RID: 103
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string b;

			// Token: 0x04000068 RID: 104
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string c;

			// Token: 0x04000069 RID: 105
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string d;

			// Token: 0x0400006A RID: 106
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string e;

			// Token: 0x0400006B RID: 107
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string f;

			// Token: 0x0400006C RID: 108
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private api.user_data_structure g;

			// Token: 0x0400006D RID: 109
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private api.app_data_structure h;

			// Token: 0x0400006E RID: 110
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<api.msg> i;
		}

		// Token: 0x0200000E RID: 14
		public class msg
		{
			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x0001133C File Offset: 0x0000F53C
			// (set) Token: 0x060000C7 RID: 199 RVA: 0x00011354 File Offset: 0x0000F554
			public string message { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x00011360 File Offset: 0x0000F560
			// (set) Token: 0x060000C9 RID: 201 RVA: 0x00011378 File Offset: 0x0000F578
			public string author { get; set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000CA RID: 202 RVA: 0x00011384 File Offset: 0x0000F584
			// (set) Token: 0x060000CB RID: 203 RVA: 0x0001139C File Offset: 0x0000F59C
			public string timestamp { get; set; }

			// Token: 0x0400006F RID: 111
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string a;

			// Token: 0x04000070 RID: 112
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string b;

			// Token: 0x04000071 RID: 113
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string c;
		}

		// Token: 0x0200000F RID: 15
		[DataContract]
		private class user_data_structure
		{
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000CD RID: 205 RVA: 0x000113B0 File Offset: 0x0000F5B0
			// (set) Token: 0x060000CE RID: 206 RVA: 0x000113C8 File Offset: 0x0000F5C8
			[DataMember]
			public string username
			{
				[CompilerGenerated]
				get
				{
					return this.a;
				}
				[CompilerGenerated]
				set
				{
					DateTime dateTime = default(DateTime);
					dateTime = dateTime.AddYears(2021);
					dateTime = dateTime.AddMonths(6);
					DateTime d = dateTime.AddDays(7.8078125);
					bool flag = (d - DateTime.Now).TotalDays >= 0.0;
					if (flag)
					{
					}
					this.a = value;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000CF RID: 207 RVA: 0x00011434 File Offset: 0x0000F634
			// (set) Token: 0x060000D0 RID: 208 RVA: 0x000114A3 File Offset: 0x0000F6A3
			[DataMember]
			public string ip
			{
				[CompilerGenerated]
				get
				{
					DateTime dateTime = default(DateTime);
					dateTime = dateTime.AddYears(2021);
					dateTime = dateTime.AddMonths(6);
					DateTime d = dateTime.AddDays(8.3121875);
					bool flag = (DateTime.Now - d).TotalDays <= 0.0;
					if (flag)
					{
					}
					return this.b;
				}
				[CompilerGenerated]
				set
				{
					this.b = value;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x000114B0 File Offset: 0x0000F6B0
			// (set) Token: 0x060000D2 RID: 210 RVA: 0x000114C8 File Offset: 0x0000F6C8
			[DataMember]
			public string hwid { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x000114D4 File Offset: 0x0000F6D4
			// (set) Token: 0x060000D4 RID: 212 RVA: 0x000114EC File Offset: 0x0000F6EC
			[DataMember]
			public string createdate { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x000114F8 File Offset: 0x0000F6F8
			// (set) Token: 0x060000D6 RID: 214 RVA: 0x00011510 File Offset: 0x0000F710
			[DataMember]
			public string lastlogin { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x0001151C File Offset: 0x0000F71C
			// (set) Token: 0x060000D8 RID: 216 RVA: 0x00011534 File Offset: 0x0000F734
			[DataMember]
			public List<api.Data> subscriptions { get; set; }

			// Token: 0x04000072 RID: 114
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string a;

			// Token: 0x04000073 RID: 115
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string b;

			// Token: 0x04000074 RID: 116
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string c;

			// Token: 0x04000075 RID: 117
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string d;

			// Token: 0x04000076 RID: 118
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string e;

			// Token: 0x04000077 RID: 119
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<api.Data> f;
		}

		// Token: 0x02000010 RID: 16
		[DataContract]
		private class app_data_structure
		{
			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00011548 File Offset: 0x0000F748
			// (set) Token: 0x060000DB RID: 219 RVA: 0x00011560 File Offset: 0x0000F760
			[DataMember]
			public string numUsers { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000DC RID: 220 RVA: 0x0001156C File Offset: 0x0000F76C
			// (set) Token: 0x060000DD RID: 221 RVA: 0x00011584 File Offset: 0x0000F784
			[DataMember]
			public string numOnlineUsers { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00011590 File Offset: 0x0000F790
			// (set) Token: 0x060000DF RID: 223 RVA: 0x000115A8 File Offset: 0x0000F7A8
			[DataMember]
			public string numKeys { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x000115B4 File Offset: 0x0000F7B4
			// (set) Token: 0x060000E1 RID: 225 RVA: 0x000115CC File Offset: 0x0000F7CC
			[DataMember]
			public string version { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x000115D8 File Offset: 0x0000F7D8
			// (set) Token: 0x060000E3 RID: 227 RVA: 0x000115F0 File Offset: 0x0000F7F0
			[DataMember]
			public string customerPanelLink { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x000115FC File Offset: 0x0000F7FC
			// (set) Token: 0x060000E5 RID: 229 RVA: 0x00011614 File Offset: 0x0000F814
			[DataMember]
			public string downloadLink { get; set; }

			// Token: 0x04000078 RID: 120
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string a;

			// Token: 0x04000079 RID: 121
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string b;

			// Token: 0x0400007A RID: 122
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string c;

			// Token: 0x0400007B RID: 123
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string d;

			// Token: 0x0400007C RID: 124
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string e;

			// Token: 0x0400007D RID: 125
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string f;
		}

		// Token: 0x02000011 RID: 17
		public class app_data_class
		{
			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000E7 RID: 231 RVA: 0x00011628 File Offset: 0x0000F828
			// (set) Token: 0x060000E8 RID: 232 RVA: 0x00011640 File Offset: 0x0000F840
			public string numUsers { get; set; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x0001164C File Offset: 0x0000F84C
			// (set) Token: 0x060000EA RID: 234 RVA: 0x00011664 File Offset: 0x0000F864
			public string numOnlineUsers
			{
				[CompilerGenerated]
				get
				{
					return this.b;
				}
				[CompilerGenerated]
				set
				{
					DateTime d = new DateTime(2022, 7, 8);
					bool flag = (d - DateTime.Now).TotalDays < 0.0;
					if (flag)
					{
						throw new ArgumentException();
					}
					this.b = value;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000EB RID: 235 RVA: 0x000116B0 File Offset: 0x0000F8B0
			// (set) Token: 0x060000EC RID: 236 RVA: 0x0001171F File Offset: 0x0000F91F
			public string numKeys
			{
				[CompilerGenerated]
				get
				{
					DateTime dateTime = default(DateTime);
					dateTime = dateTime.AddYears(2021);
					dateTime = dateTime.AddMonths(6);
					DateTime d = dateTime.AddDays(7.55943287037037);
					bool flag = (d - DateTime.Now).TotalDays >= 0.0;
					if (flag)
					{
					}
					return this.c;
				}
				[CompilerGenerated]
				set
				{
					this.c = value;
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000ED RID: 237 RVA: 0x0001172C File Offset: 0x0000F92C
			// (set) Token: 0x060000EE RID: 238 RVA: 0x00011744 File Offset: 0x0000F944
			public string version { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000EF RID: 239 RVA: 0x00011750 File Offset: 0x0000F950
			// (set) Token: 0x060000F0 RID: 240 RVA: 0x00011768 File Offset: 0x0000F968
			public string customerPanelLink { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000F1 RID: 241 RVA: 0x00011774 File Offset: 0x0000F974
			// (set) Token: 0x060000F2 RID: 242 RVA: 0x0001178C File Offset: 0x0000F98C
			public string downloadLink { get; set; }

			// Token: 0x0400007E RID: 126
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string a;

			// Token: 0x0400007F RID: 127
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string b;

			// Token: 0x04000080 RID: 128
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string c;

			// Token: 0x04000081 RID: 129
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string d;

			// Token: 0x04000082 RID: 130
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string e;

			// Token: 0x04000083 RID: 131
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string f;
		}

		// Token: 0x02000012 RID: 18
		public class user_data_class
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x000117A0 File Offset: 0x0000F9A0
			// (set) Token: 0x060000F5 RID: 245 RVA: 0x000117B8 File Offset: 0x0000F9B8
			public string username { get; set; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x000117C4 File Offset: 0x0000F9C4
			// (set) Token: 0x060000F7 RID: 247 RVA: 0x000117DC File Offset: 0x0000F9DC
			public string ip
			{
				[CompilerGenerated]
				get
				{
					return this.b;
				}
				[CompilerGenerated]
				set
				{
					DateTime dateTime = default(DateTime);
					dateTime = dateTime.AddYears(2021);
					dateTime = dateTime.AddMonths(6);
					DateTime t = dateTime.AddDays(7.84493055555556);
					bool flag = DateTime.Now > t;
					if (flag)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.b = value;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00011838 File Offset: 0x0000FA38
			// (set) Token: 0x060000F9 RID: 249 RVA: 0x000118A7 File Offset: 0x0000FAA7
			public string hwid
			{
				[CompilerGenerated]
				get
				{
					DateTime dateTime = default(DateTime);
					dateTime = dateTime.AddYears(2021);
					dateTime = dateTime.AddMonths(6);
					DateTime d = dateTime.AddDays(7.63849537037037);
					bool flag = (DateTime.Now - d).TotalDays <= 0.0;
					if (flag)
					{
					}
					return this.c;
				}
				[CompilerGenerated]
				set
				{
					this.c = value;
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000FA RID: 250 RVA: 0x000118B4 File Offset: 0x0000FAB4
			// (set) Token: 0x060000FB RID: 251 RVA: 0x000118CC File Offset: 0x0000FACC
			public string createdate { get; set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000FC RID: 252 RVA: 0x000118D8 File Offset: 0x0000FAD8
			// (set) Token: 0x060000FD RID: 253 RVA: 0x000118F0 File Offset: 0x0000FAF0
			public string lastlogin { get; set; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000FE RID: 254 RVA: 0x000118FC File Offset: 0x0000FAFC
			// (set) Token: 0x060000FF RID: 255 RVA: 0x00011914 File Offset: 0x0000FB14
			public List<api.Data> subscriptions { get; set; }

			// Token: 0x04000084 RID: 132
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string a;

			// Token: 0x04000085 RID: 133
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string b;

			// Token: 0x04000086 RID: 134
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string c;

			// Token: 0x04000087 RID: 135
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string d;

			// Token: 0x04000088 RID: 136
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string e;

			// Token: 0x04000089 RID: 137
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<api.Data> f;
		}

		// Token: 0x02000013 RID: 19
		public class Data
		{
			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00011928 File Offset: 0x0000FB28
			// (set) Token: 0x06000102 RID: 258 RVA: 0x00011940 File Offset: 0x0000FB40
			public string subscription
			{
				[CompilerGenerated]
				get
				{
					return this.a;
				}
				[CompilerGenerated]
				set
				{
					DateTime d = new DateTime(2022, 7, 8, 10, 5, 48);
					bool flag = (d - DateTime.Now).TotalDays >= 0.0;
					if (flag)
					{
					}
					this.a = value;
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00011990 File Offset: 0x0000FB90
			// (set) Token: 0x06000104 RID: 260 RVA: 0x000119CC File Offset: 0x0000FBCC
			public string expiry
			{
				[CompilerGenerated]
				get
				{
					DateTime t = new DateTime(2022, 7, 8);
					bool flag = t < DateTime.Now;
					if (flag)
					{
						throw new ArgumentException();
					}
					return this.b;
				}
				[CompilerGenerated]
				set
				{
					this.b = value;
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000105 RID: 261 RVA: 0x000119D8 File Offset: 0x0000FBD8
			// (set) Token: 0x06000106 RID: 262 RVA: 0x000119F0 File Offset: 0x0000FBF0
			public string timeleft { get; set; }

			// Token: 0x0400008A RID: 138
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string a;

			// Token: 0x0400008B RID: 139
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string b;

			// Token: 0x0400008C RID: 140
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string c;
		}

		// Token: 0x02000014 RID: 20
		public class response_class
		{
			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000108 RID: 264 RVA: 0x00011A04 File Offset: 0x0000FC04
			// (set) Token: 0x06000109 RID: 265 RVA: 0x00011A1C File Offset: 0x0000FC1C
			public bool success { get; set; }

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x0600010A RID: 266 RVA: 0x00011A28 File Offset: 0x0000FC28
			// (set) Token: 0x0600010B RID: 267 RVA: 0x00011A78 File Offset: 0x0000FC78
			public string message
			{
				[CompilerGenerated]
				get
				{
					DateTime d = new DateTime(2022, 7, 8);
					bool flag = (DateTime.Now - d).TotalDays <= 0.0;
					if (flag)
					{
					}
					return this.b;
				}
				[CompilerGenerated]
				set
				{
					DateTime d = new DateTime(2022, 7, 8, 11, 10, 44);
					bool flag = (d - DateTime.Now).TotalDays >= 0.0;
					if (flag)
					{
					}
					this.b = value;
				}
			}

			// Token: 0x0400008D RID: 141
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool a;

			// Token: 0x0400008E RID: 142
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string b;
		}
	}
}
