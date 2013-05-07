using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Simple.HostsManager
{
    public interface IHostsFileToObjectConverter
    {
        HostsEntry FromString(string hostEntry);
        string ToString(HostsEntry hostsEntry);
    }

    public class HostsFileToObjectConverter : IHostsFileToObjectConverter
    {
        private static readonly Regex HostRowPattern = new Regex(@"^#?\s*"
                                                                 + @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}|[0-9a-f:]+)\s+"
                                                                 + @"(?<hosts>(([a-z0-9][-_a-z0-9]*\.?)+\s*)+)"
                                                                 + @"(?:#\s*(?<comment>.*?)\s*)?$",
                                                                 RegexOptions.IgnoreCase | RegexOptions.Singleline |
                                                                 RegexOptions.Compiled);

        public HostsEntry FromString(string hostEntry)
        {
            try
            {
                var match = HostRowPattern.Match(hostEntry);
                if (!match.Success) throw new FormatException();
                if (hostEntry[0] == '#') throw new FormatException();

                return new HostsEntry
                    {
                        IpAddress = IPAddress.Parse(match.Groups["ip"].Value),
                        HostNames = (match.Groups["hosts"].Value)
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                            .ToList(),
                        Comment = match.Groups["comment"].Value
                    };
            }
            catch
            {
                return new HostsEntry
                    {
                        IpAddress = null,
                        HostNames = null,
                        Comment = null
                    };
            }
        }

        public string ToString(HostsEntry hostsEntry)
        {
            if (!hostsEntry.IsValid())
                throw new Exception("Unable to parse invalid HostsEntry");

            var result = string.Format("{0}\t{1}\t",
                                       hostsEntry.IpAddress,
                                       HostNamesToString(hostsEntry.HostNames));
            if (!string.IsNullOrEmpty(hostsEntry.Comment)) result += "# " + hostsEntry.Comment;
            return result.Trim();
        }

        public string HostNamesToString(IEnumerable<string> hostNames)
        {
            return String.Join(" ", hostNames.ToArray());   
        }
    }
}