using System.Collections.Generic;
using System.Linq;
using System.Net;

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
        private readonly HostsFileWriter _hostsFileWriter;
        private readonly HostsFileToObjectConverter _hostsFileToObjectConverter;
        private readonly HostsFileRetriever _hostsFileRetriever;

        public HostsContext()
        {
            _hostsFileWriter = new HostsFileWriter();
            _hostsFileToObjectConverter = new HostsFileToObjectConverter();
            _hostsFileRetriever = new HostsFileRetriever();
        }

        public List<HostsEntry> All()
        {
            var lines = _hostsFileRetriever.GetHostsByLine();

            return lines.Select(_hostsFileToObjectConverter.FromString).Where(x => x.IsValid()) as List<HostsEntry>;
        }

        public void Add(HostsEntry hostsEntry)
        {
            _hostsFileWriter.AppendLine(hostsEntry);
        }

        public void Add(string hostName, string ipAddress)
        {
            var hostEntry = new HostsEntry
                {
                    HostNames = new List<string> { hostName },
                    IpAddress = IPAddress.Parse(ipAddress)
                };

            _hostsFileWriter.AppendLine(hostEntry);
        }

        public void Add(string hostName, string ipAddress, string comment = "")
        {
            var hostEntry = new HostsEntry
            {
                HostNames = new List<string> { hostName },
                IpAddress = IPAddress.Parse(ipAddress),
                Comment = comment
            };

            _hostsFileWriter.AppendLine(hostEntry);
        }

        public void RemoveByHostName(string hostName)
        {
            _hostsFileWriter.RemoveLine(hostName);
        }

        public void Remove(HostsEntry hostsEntry)
        {
            _hostsFileWriter.RemoveLine(_hostsFileToObjectConverter.ToString(hostsEntry));
        }
    }
}