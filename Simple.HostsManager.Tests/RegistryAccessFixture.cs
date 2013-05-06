using System.Text;
using NUnit.Framework;

namespace Simple.HostsManager.Tests
{
    [TestFixture]
    public class RegistryAccessFixture
    {
        [Test]
        public void CanRetrieveHostsFilename()
        {
            var hostsFileAccess = new HostsFileAccess();
            var fileName = hostsFileAccess.GetFileName();

            Assert.IsNotNullOrEmpty(fileName);
        }

        [Test]
        public void CanWritetoHostsFile()
        {
            var hostsFileAccess = new HostsFileAccess();
            var hasAccess = hostsFileAccess.HasWriteAccess();

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
