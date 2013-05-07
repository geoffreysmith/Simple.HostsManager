using NUnit.Framework;

namespace Simple.HostsManager.Tests
{
    [TestFixture]
    public class RegistryAccessFixture
    {
        private readonly HostsFileAccess _hostsFileAccess;

        public RegistryAccessFixture()
        {
            _hostsFileAccess = new HostsFileAccess();
        }

        [Test]
        public void CanRetrieveHostsFilename()
        {
            var fileName = _hostsFileAccess.GetFileName();

            Assert.IsNotNullOrEmpty(fileName);
        }

        [Test]
        public void CanWritetoHostsFile()
        {
            var hasAccess = _hostsFileAccess.HasWriteAccess();

            Assert.IsTrue(hasAccess);
        }

        [Test]
        public void CanRetrieveHostsFileEntries()
        {
            var hostsRetriever = new HostsFileRetriever();
            var lines = hostsRetriever.GetHostsByLine();

            Assert.IsNotEmpty(lines);
        }
    }
}
