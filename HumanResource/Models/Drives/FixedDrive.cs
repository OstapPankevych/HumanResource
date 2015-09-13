using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Models.Drives
{
    public class FixedDrive
    {
        public IEnumerable<string> Folders { get; set; }
        public IEnumerable<string> Files { get; set; }
        public string CurrentPath { get; set; }
        public int CountFilesLessThat_10mb { get; set; }
        public int CountFiles_10_50mb { get; set; }
        public int CountFilesMoreThat_100mb { get; set; }
    }
}
