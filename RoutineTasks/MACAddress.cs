using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using System.Reflection;

namespace RoutineTasks
{
    public enum SetMacParam { none,resetdefault,donotresetinterface}

    public class MACAddress
    {
        static string SCRIPT_NAME = @"New-MACaddress.ps1";

        public static string GetMAC() 
        {
            string physicalAdress = string.Empty;
            ManagementObjectCollection queryCollection = null;
            ManagementScope theScope = null;
            theScope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            StringBuilder theQueryBuilder = new StringBuilder();
            theQueryBuilder.Append("SELECT * FROM Win32_NetworkAdapter");
            ObjectQuery theQuery = new ObjectQuery(theQueryBuilder.ToString());
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(theScope, theQuery);
            queryCollection = searcher.Get();

            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["MacAddress"] != null)
                {
                    return mo["MacAddress"].ToString();
                }
            }
            return null;
        }

        public static string GenerateMAC()
        {
            string NewMac = string.Empty;
            var r = new Random();
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += ":";
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += ":";
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += ":";
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += ":";
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += ":";
            NewMac += r.Next(0, 16).ToString("X");
            NewMac += r.Next(0, 16).ToString("X");

            return NewMac;
        }

        /// <summary>
        /// To select a random MAC address with a valid vendor ID number, and either assign the MAC to the sole physical interface, or, 
        /// if there are multiple interfaces, prompt the user to select the desired interface:
        /// new-macaddress.ps1
        ///
        /// To delete the registry value for the custom MAC address so that the built-in MAC of the NIC will be used instead (revert to factory default):
        /// new-macaddress.ps1 -resetdefault
        ///
        /// To modify the registry, but not disable and enable the NIC, and not release or renew any DHCP leases:
        /// new-macaddress.ps1 -donotresetinterface
        ///
        /// Note: If you examine the source code, you'll find a few other options for the random-mac() function to play with.
        /// </summary>
        public static void SetMAC(SetMacParam ScrptParameter = SetMacParam.none) {

            string scrptName = String.Concat(Path.GetDirectoryName(Assembly.GetAssembly(typeof(MACAddress)).Location), "\\", SCRIPT_NAME);
            // create Powershell runspace
            string parameter = string.Empty;
            switch (ScrptParameter){ 
                case SetMacParam.resetdefault: parameter = "-resetdefault"; break;
                case SetMacParam.donotresetinterface: parameter = "-donotresetinterface"; break;
            }

            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            RunspaceInvoke runSpaceInvoker = new RunspaceInvoke(runspace);

            // create a pipeline and feed it the script text
            Pipeline pipeline = runspace.CreatePipeline();
            Command command = new Command(scrptName);
            command.Parameters.Add(null, parameter);
            pipeline.Commands.Add(command);

            pipeline.Invoke();
            runspace.Close();
        }
    }
}
