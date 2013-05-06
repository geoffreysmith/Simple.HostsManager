using System;
using System.IO;
using System.Text;

namespace Simple.HostsManager
{
    public class HostsFileWriter
    {
        public void AppendLine(string input)
        {
            var hostsFileAccess = new HostsFileAccess();

            var file = new FileStream(hostsFileAccess.GetFileName(), FileMode.Append, FileAccess.Write);
            var writer = new StreamWriter(file, Encoding.Default);
            
            writer.WriteLine(Environment.NewLine + input);

            writer.Close();
        }

        public void WriteHostsFile(string fileToWrite)
        {
            var hostsFileAccess = new HostsFileAccess();

            var file = new FileStream(hostsFileAccess.GetFileName(), FileMode.Open, FileAccess.Read);

            var writer = new StreamWriter(file, Encoding.Default);

            writer.Write(fileToWrite);

            writer.Close();
        }

        public void RemoveLine(string lineToDelete)
        {
            var hostsFileAccess = new HostsFileAccess();

            var file = new FileStream(hostsFileAccess.GetFileName(), FileMode.Open, FileAccess.Read);

            var reader = new StreamReader(file, Encoding.Default);

			string line;
            var newHostsFile = string.Empty;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(lineToDelete))
                    continue;

                newHostsFile += line;
                newHostsFile += Environment.NewLine;
            }


        }
    }
}