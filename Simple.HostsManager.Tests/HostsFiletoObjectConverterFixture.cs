using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;

namespace Simple.HostsManager.Tests
{
    [TestFixture]
    public class HostsFileWriterFixture
    {
        [TestCase("127.0.0.1	local.writetest.net")]
        public void CanWriteLineToHostsFile(string input)
        {
            var hostsFileWriter = new HostsFileWriter();
            hostsFileWriter.AppendLine(input);

            var hostFileRetriever = new HostsFileRetriever();
            var lines = hostFileRetriever.GetHostsByLine();

            Assert.IsTrue(lines.Any(x => x == input));
        }

        [TestCase("127.0.0.1	local.writetest.net")]
        public void CanRemoveLineFromHostsFile(string input)
        {
            var hostsFileWriter = new HostsFileWriter();
            hostsFileWriter.RemoveLine(input);

            var hostFileRetriever = new HostsFileRetriever();
            var lines = hostFileRetriever.GetHostsByLine();

            Assert.IsFalse(lines.Any(x => x == input));
        }

    }

    [TestFixture]
    public class HostsFiletoObjectConverterFixture
    {
        [TestCase("127.0.0.1	local.test.net")]
        public void CanConvertLineToValidObject(string input)
        {
            var hostsFileToObjectConverter = new HostsFileToObjectConverter();
            var hostEntry = hostsFileToObjectConverter.FromString(input);

            Assert.IsNotNull(hostEntry);
            Assert.IsTrue(hostEntry.IsValid());
        }

        [TestCase("Invalid string")]
        public void CanConvertLineToInvalidObject(string input)
        {
            var hostsFileToObjectConverter = new HostsFileToObjectConverter();
            var hostEntry = hostsFileToObjectConverter.FromString(input);

            Assert.IsNotNull(hostEntry);
            Assert.IsFalse(hostEntry.IsValid());
        }

        [TestCase("#127.0.0.1	local.test.net")]
        public void CanConvertLineToValidObjecDisabledt(string input)
        {
            var hostsFileToObjectConverter = new HostsFileToObjectConverter();
            var hostEntry = hostsFileToObjectConverter.FromString(input);

            Assert.IsNotNull(hostEntry);
            Assert.IsFalse(hostEntry.IsEnabled());
            Assert.IsTrue(hostEntry.IsValid());
        }

        [TestCase("127.0.0.1    local.test.net #this is a comment")]
        public void CanConvertLineToValidObjectWithComment(string input)
        {
            var hostsFileToObjectConverter = new HostsFileToObjectConverter();
            var hostEntry = hostsFileToObjectConverter.FromString(input);

            Assert.IsNotNull(hostEntry);
            Assert.AreEqual("this is a comment", hostEntry.Comment);
            Assert.IsTrue(hostEntry.IsValid());
        }

        [Test]
        public void CanConvertValidObjectToString()
        {
            var hostsEntry = new HostsEntry
                {
                    Comment = string.Empty,
                    HostNames = new List<string> {"local.test.net"},
                    IpAddress = IPAddress.Parse("127.0.0.1")
                };

            var hostsFileToObjectConverter = new HostsFileToObjectConverter();
            var parsedEntry = hostsFileToObjectConverter.ToString(hostsEntry);

            Assert.AreEqual(string.Format("{0}\t{1}", "127.0.0.1", "local.test.net"), parsedEntry);
        }
        
    }
}