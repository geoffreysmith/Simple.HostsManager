using System.Collections.Generic;
using System.Net;
using NUnit.Framework;

namespace Simple.HostsManager.Tests
{
    [TestFixture]
    public class HostsFiletoObjectConverterFixture
    {
        private readonly HostsFileToObjectConverter _hostsFileToObjectConverter;

        public HostsFiletoObjectConverterFixture()
        {
            _hostsFileToObjectConverter = new HostsFileToObjectConverter();
        }

        [TestCase("127.0.0.1	local.test.net")]
        public void CanConvertLineToValidObject(string input)
        {
            var hostEntry = _hostsFileToObjectConverter.FromString(input);

            Assert.IsNotNull(hostEntry);
            Assert.IsTrue(hostEntry.IsValid());
        }

        [TestCase("Invalid string")]
        public void CanConvertLineToInvalidObject(string input)
        {
            var hostEntry = _hostsFileToObjectConverter.FromString(input);

            Assert.IsNotNull(hostEntry);
            Assert.IsFalse(hostEntry.IsValid());
        }

        [TestCase("#127.0.0.1	local.test.net")]
        public void CanConvertLineToValidObjecDisabledt(string input)
        {
            var hostEntry = _hostsFileToObjectConverter.FromString(input);

            Assert.IsNotNull(hostEntry);
            Assert.IsFalse(hostEntry.IsValid());
        }

        [TestCase("127.0.0.1    local.test.net #this is a comment")]
        public void CanConvertLineToValidObjectWithComment(string input)
        {
            var hostEntry = _hostsFileToObjectConverter.FromString(input);

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

            var parsedEntry = _hostsFileToObjectConverter.ToString(hostsEntry);

            Assert.AreEqual(string.Format("{0}\t{1}", "127.0.0.1", "local.test.net"), parsedEntry);
        }
        
    }
}