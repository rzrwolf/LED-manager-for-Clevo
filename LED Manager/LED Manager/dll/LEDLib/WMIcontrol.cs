using System;
using System.IO;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;

namespace LEDLib
{
	public class WMIcontrol
	{
		
		public void InitWMI()
		{					
			CheckClevoMof ();				
			CheckMofReg();

			if (this.classInstance == null)
			{
				this.classInstance = new ManagementObject("root\\WMI", "CLEVO_GET.InstanceName='ACPI\\PNP0C14\\0_0'", null);
			}

		}

				
		private void CheckClevoMof () {
					if(File.Exists("C:\\Windows\\SysWOW64\\clevomof.dll") ==false) {
					System.Diagnostics.Debug.WriteLine("clevomof.dll not found, trying to add");
					try{
						File.WriteAllBytes("C:\\Windows\\SysWOW64\\clevomof.dll", (byte[])Resources.ResourceManager.GetObject("clevomof"));
						MessageBox.Show("You didn't have the necessary library clevomof.dll in C:\\Windows\\SysWOW64. It was added automatically. PLEASE RESTART WINDOWS!","Clevomof.dll successfully added to system.",MessageBoxButtons.OK, MessageBoxIcon.Warning);													

						}
					catch {
						System.Diagnostics.Debug.WriteLine("Bad, failed to wrtie clevomof.dll, check admin privelege");
						MessageBox.Show("You dont't have the necessary DLL library 'clevomof.dll' in C:\\Windows\\SysWOW64." +
						"Program failed to write clevomof.dll, check admin privelege. File was saved in program's folder." +
						"Please copy it manually to C:\\Windows\\SysWOW64 and RESTART WINDOWS!","Clevomof.dll write fail, no access.",MessageBoxButtons.OK, MessageBoxIcon.Error);
						string _execPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
						File.WriteAllBytes(_execPath+"\\clevomof.dll", (byte[])Resources.ResourceManager.GetObject("clevomof"));	
						
						}
					}
					else {
						System.Diagnostics.Debug.WriteLine("All good, clevomof.dll found");
					}
		
		}
		
		
		private void CheckMofReg()
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\WmiAcpi\\", true);
				string a = registryKey.GetValue("MofImagePath", "").ToString();
				if (a == "" || a != "syswow64\\clevomof.dll")
				{
					registryKey.SetValue("MofImagePath", "syswow64\\clevomof.dll", RegistryValueKind.String);
					MessageBox.Show("You didn't have the necessary registry record for clevomof.dll in SYSTEM\\CurrentControlSet\\Services\\WmiAcpi\\. It was added automatically. PLEASE RESTART WINDOWS!","Registry record for Clevomof.dll successfully added to system.",MessageBoxButtons.OK, MessageBoxIcon.Warning);													

				}
				registryKey.Close();
			}
			catch
			{
				MessageBox.Show("You dont't have the necessary registry record for clevomof.dll in SYSTEM\\CurrentControlSet\\Services\\WmiAcpi\\. Program failed to write registry, check admin privelege.", "Failed to write tegistry", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
			
		
		//async method with delay for WMI calls to avoid WMI going crazy
		public  async void  InvokeMethod(string CommmandData , string CommandName)
		{	
									counter = counter+300;
			                     	Task delay = Task.Delay(counter);
			                     	await delay;
		                     	
			                     	InvokeDelayedMethod(CommmandData,CommandName);
			                     	counter = counter -300;	
			}		

		public async void InvokeDelayedMethod(string CommmandData , string CommandName)
		{

			Task delay = Task.Delay(200);  
			await delay;			            
			System.Diagnostics.Debug.WriteLine("WMI CALL");
			try
			{
				uint uCommmandData = Convert.ToUInt32(CommmandData);
				this.inParams = this.classInstance.GetMethodParameters(CommandName);
				this.inParams["Data"] = uCommmandData;
				this.outParams = this.classInstance.InvokeMethod(CommandName, this.inParams, null);
			}
			catch 
			{
			}

		}
		private int counter = 0;

		private ManagementObject classInstance;


		private ManagementBaseObject outParams;

		private ManagementBaseObject inParams;




	}
}
