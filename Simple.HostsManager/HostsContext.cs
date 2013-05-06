using System.Collections.Generic;
using System.Linq;

namespace Simple.HostsManager
{
    public interface IHostsContext
    {
        List<HostsEntry> All();
        void Add(HostsEntry hostsEntry);
        void Add(string hostName, string ipAddress);
        void Add(string hostName, string ipAddress, string comment);
        void RemoveByHostName(string hostName);
    }

    public class HostsContext : IHostsContext
    {
        public List<HostsEntry> All()
        {
            var hostsFileToObject = new HostsFileToObjectConverter();
            var hostsFileRetriever = new HostsFileRetriever();
            var lines = hostsFileRetriever.GetHostsByLine();

            return lines.Select(hostsFileToObject.FromString).Where(x=> x.IsValid()) as List<HostsEntry>;
        }

        public void Add(HostsEntry hostsEntry)
        {
            throw new System.NotImplementedException();
        }

        public void Add(string hostName, string ipAddress)
        {
            throw new System.NotImplementedException();
        }

        public void Add(string hostName, string ipAddress, string comment = "")
        {
            throw new System.NotImplementedException();
        }

        public void RemoveByHostName(string hostName)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(HostsEntry hostsEntry)
        {
            throw new System.NotImplementedException();
        }
    }
}