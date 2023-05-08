namespace Lean.Forms
{
	// Token: 0x02000009 RID: 9
	public partial class Main : global::System.Windows.Forms.Form
	{
		// Token: 0x06000093 RID: 147 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.m_e != null;
			if (flag)
			{
				this.m_e.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400001A RID: 26
		private global::System.ComponentModel.IContainer m_e = null;
	}
}
