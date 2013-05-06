using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Simple.HostsManager
{
    public class HostsFileRetriever
    {
        public IEnumerable<string> GetHostsByLine()
        {
            var file = File.ReadAllBytes(new HostsFileAccess().GetFileName());
            var reader = new StreamReader(new MemoryStream(file), Encoding.Default);
            
            string line;

            while ((line = reader.ReadLine()) != null)
                yield return line;
        }
    }
}