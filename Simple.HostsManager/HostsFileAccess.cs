using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace Simple.HostsManager
{
    public class HostsFileAccess
    {
        private const string RegistryPathToHostsFile = @"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\";

        public string GetFileName()
        {
            var hostsRegKey = Registry.LocalMachine.OpenSubKey(RegistryPathToHostsFile);

            if (hostsRegKey == null) throw new Exception("Cannot find registry key for hosts file in " + RegistryPathToHostsFile);

            var hostsPath = hostsRegKey.GetValue("DataBasePath") as string;
            hostsRegKey.Close();

            if (hostsPath != null && hostsPath.Trim() == "") throw new FileNotFoundException("Empty path");

            return Environment.ExpandEnvironmentVariables(hostsPath + @"\hosts");
        }

        public bool HasWriteAccess()
        {
            var hostsPermissions = new FileIOPermission(FileIOPermissionAccess.AllAccess, GetFileName());

            try
            {
                hostsPermissions.Demand();
                return true;
            }
            catch (SecurityException)
            {
                return false;
            }
        }
    }
}