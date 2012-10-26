using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHosted
{
    public class FileResult
    {
        public IEnumerable<string> FileNames { get; set; }

        public string Submitter { get; set; }
    }
}
