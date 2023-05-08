using System;
using System.Collections.Specialized;
using System.Net;

// Token: 0x02000003 RID: 3
internal class e
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002404 File Offset: 0x00000604
	public static byte[] a(string a, NameValueCollection b)
	{
		byte[] result;
		using (WebClient webClient = new WebClient())
		{
			result = webClient.UploadValues(a, b);
		}
		return result;
	}
}
