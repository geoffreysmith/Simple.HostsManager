using System.Linq;
using NUnit.Framework;

namespace Simple.HostsManager.Tests
{
    [TestFixture]
    public class HostsFileWriterFixture
    {
        private readonly HostsFileWriter _hostsFileWriter;
        private readonly HostsFileRetriever _hostFileRetriever;
        private readonly HostsFileToObjectConverter _hostsFileToObjectConverter;

        public HostsFileWriterFixture()
        {
            _hostFileRetriever = new HostsFileRetriever();
            _hostsFileWriter = new HostsFileWriter();
            _hostsFileToObjectConverter = new HostsFileToObjectConverter();
        }

        [TestCase("127.0.0.1	local.writetest.net")]
        public void CanWriteLineToHostsFile(string input)
        {
            _hostsFileWriter.AppendLine(_hostsFileToObjectConverter.FromString(input));

            var lines = _hostFileRetriever.GetHostsByLine();

            Assert.IsTrue(lines.Any(x => x == input));
        }

        [TestCase("127.0.0.1	local.writetest.net")]
        public void CanRemoveLineFromHostsFile(string input)
        {
            _hostsFileWriter.AppendLine(_hostsFileToObjectConverter.FromString(input));

            var lines = _hostFileRetriever.GetHostsByLine();

            Assert.IsFalse(lines.Any(x => x == input));
        }

    }
}