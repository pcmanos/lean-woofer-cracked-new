using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LeanSoftware.Properties
{
	// Token: 0x0200000C RID: 12
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00010FFF File Offset: 0x0000F1FF
		public static Settings Default
		{
			get
			{
				return Settings.a;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00011008 File Offset: 0x0000F208
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00011070 File Offset: 0x0000F270
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string username
		{
			get
			{
				DateTime dateTime = default(DateTime);
				dateTime = dateTime.AddYears(2021);
				dateTime = dateTime.AddMonths(6);
				DateTime t = dateTime.AddDays(7.57686342592593);
				bool flag = !(DateTime.Now > t);
				if (flag)
				{
				}
				return (string)this["username"];
			}
			set
			{
				DateTime dateTime = default(DateTime);
				dateTime = dateTime.AddYears(2021);
				dateTime = dateTime.AddMonths(6);
				DateTime t = dateTime.AddDays(8.28049768518519);
				bool flag = t < DateTime.Now;
				if (flag)
				{
					throw new ArgumentException();
				}
				this["username"] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000110D0 File Offset: 0x0000F2D0
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000110F2 File Offset: 0x0000F2F2
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string password
		{
			get
			{
				return (string)this["password"];
			}
			set
			{
				this["password"] = value;
			}
		}

		// Token: 0x04000065 RID: 101
		private static Settings a;
	}
}
