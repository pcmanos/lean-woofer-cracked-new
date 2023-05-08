using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Lean.Resources
{
	// Token: 0x02000008 RID: 8
	public class json_wrapper
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003EAC File Offset: 0x000020AC
		public static bool is_serializable(Type to_check)
		{
			return to_check.IsSerializable || to_check.IsDefined(typeof(DataContractAttribute), true);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003EDC File Offset: 0x000020DC
		public json_wrapper(object obj_to_work_with)
		{
			this.b = obj_to_work_with;
			Type type = this.b.GetType();
			this.a = new DataContractJsonSerializer(type);
			bool flag = !json_wrapper.is_serializable(type);
			if (flag)
			{
				throw new Exception(string.Format("the object {0} isn't a serializable", this.b));
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003F34 File Offset: 0x00002134
		public object string_to_object(string json)
		{
			DateTime t = new DateTime(2022, 7, 8, 16, 41, 14);
			bool flag = !(t < DateTime.Now);
			if (flag)
			{
			}
			byte[] bytes = Encoding.Default.GetBytes(json);
			object result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = this.a.ReadObject(memoryStream);
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003FB0 File Offset: 0x000021B0
		public T string_to_generic<T>(string json)
		{
			DateTime d = new DateTime(2022, 7, 9, 1, 25, 58);
			bool flag = (d - DateTime.Now).TotalDays >= 0.0;
			if (flag)
			{
			}
			return (T)((object)this.string_to_object(json));
		}

		// Token: 0x04000011 RID: 17
		private DataContractJsonSerializer a;

		// Token: 0x04000012 RID: 18
		private object b;
	}
}
