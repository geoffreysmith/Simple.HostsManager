using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Simple.HostsManager
{
    public class HostsEntry
    {
        public IPAddress IpAddress { get; set; }
        public List<string> HostNames { get; set; }
        public string Comment { get; set; }

        public bool IsEnabled()
        {
            return IsValid() && HostNames[0] != "#";
        }

        public bool IsValid()
        {
            return IpAddress != null && HostNames.Any();
        }
    }
}