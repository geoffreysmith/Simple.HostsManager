using System;
using System.IO;
using System.Text;

namespace Simple.HostsManager
{
    public class HostsFileWriter
    {
        private readonly HostsFileAccess _hostsFileAccess;
        private readonly HostsFileToObjectConverter _hostsFileToObjectConverter;

        public HostsFileWriter()
        {
            _hostsFileAccess = new HostsFileAccess();
            _hostsFileToObjectConverter = new HostsFileToObjectConverter();
        }

        private void AppendLine(string input)
        {
            var file = new FileStream(_hostsFileAccess.GetFileName(), FileMode.Append, FileAccess.Write);
            var writer = new StreamWriter(file, Encoding.Default);
            
            writer.WriteLine(Environment.NewLine + input);

            writer.Close();
        }

        public void AppendLine(HostsEntry hostsEntry)
        {
            if (!hostsEntry.IsValid())
                throw new Exception("Cannot add invalid hosts entry");

            var hostEntryString = _hostsFileToObjectConverter.ToString(hostsEntry);

            AppendLine(hostEntryString);
        }

        public void WriteHostsFile(string fileToWrite)
        {
            var file = new FileStream(_hostsFileAccess.GetFileName(), FileMode.Open, FileAccess.Read);
            var writer = new StreamWriter(file, Encoding.Default);

            writer.Write(fileToWrite);

            writer.Close();
        }

        public void RemoveLine(string lineToDelete)
        {
            var file = new FileStream(_hostsFileAccess.GetFileName(), FileMode.Open, FileAccess.Read);
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

            WriteHostsFile(newHostsFile);
        }
    }
}