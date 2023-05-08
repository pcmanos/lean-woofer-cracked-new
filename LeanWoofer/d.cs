using System;
using Microsoft.Win32;

// Token: 0x02000002 RID: 2
internal class d
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public void b()
	{
		this.m_a = "pBuU91Vl9qHzPee3zGd7";
		for (int i = 0; i < this.m_c.Length; i++)
		{
			this.c(i);
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000208C File Offset: 0x0000028C
	private void c(int a)
	{
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(this.m_c[a], true);
		bool flag = registryKey != null;
		if (flag)
		{
			int num = 0;
			while (num < this.m_d.GetLength(1) && !(this.m_d[a, num] == "nop"))
			{
				registryKey.SetValue(this.m_d[a, num], this.m_a);
				this.m_a = "Gnaqk5MlQeuWinW2qQ4M";
				num++;
			}
			registryKey.Close();
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002120 File Offset: 0x00000320
	public d()
	{
		string[,] array = new string[7, 7];
		array[0, 0] = "SystemProductName";
		array[0, 1] = "Identifier";
		array[0, 2] = "Previous Update Revision";
		array[0, 3] = "ProcessorNameString";
		array[0, 4] = "VendorIdentifier";
		array[0, 5] = "Platform Specific Field1";
		array[0, 6] = "Component Information";
		array[1, 0] = "SerialNumber";
		array[1, 1] = "Identifier";
		array[1, 2] = "SystemManufacturer";
		array[1, 3] = "nop";
		array[1, 4] = "nop";
		array[1, 5] = "nop";
		array[1, 6] = "nop";
		array[2, 0] = "ComputerHardwareId";
		array[2, 1] = "ComputerHardwareIds";
		array[2, 2] = "BIOSVendor";
		array[2, 3] = "ProductId";
		array[2, 4] = "ProcessorNameString";
		array[2, 5] = "BIOSReleaseDate";
		array[2, 6] = "nop";
		array[3, 0] = "ProductId";
		array[3, 1] = "InstallDate";
		array[3, 2] = "InstallTime";
		array[3, 3] = "nop";
		array[3, 4] = "nop";
		array[3, 5] = "nop";
		array[3, 6] = "nop";
		array[4, 0] = "SusClientId";
		array[4, 1] = "nop";
		array[4, 2] = "nop";
		array[4, 3] = "nop";
		array[4, 4] = "nop";
		array[4, 5] = "nop";
		array[4, 6] = "nop";
		array[5, 0] = "NetCfgInstanceId";
		array[5, 1] = "NetLuidIndex";
		array[5, 2] = "nop";
		array[5, 3] = "nop";
		array[5, 4] = "nop";
		array[5, 5] = "nop";
		array[5, 6] = "nop";
		array[6, 0] = "NetworkAddress";
		array[6, 1] = "NetCfgInstanceId";
		array[6, 2] = "NetworkInterfaceInstallTimestamp";
		array[6, 3] = "nop";
		array[6, 4] = "nop";
		array[6, 5] = "nop";
		array[6, 6] = "nop";
		this.m_d = array;
		base..ctor();
	}

	// Token: 0x04000001 RID: 1
	private string m_a;

	// Token: 0x04000002 RID: 2
	private string[] m_c = new string[]
	{
		"Hardware\\Description\\System\\CentralProcessor\\0",
		"HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0",
		"SYSTEM\\CurrentControlSet\\Control\\SystemInformation",
		"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
		"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate",
		"SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0001",
		"SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0012"
	};

	// Token: 0x04000003 RID: 3
	private string[,] m_d;
}
