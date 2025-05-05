using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Demand_Paging
{
    public class Job
    {
        public int JobNumber { get; set; }
        public int Size { get; set; }
        public List<Page> Pages { get; set; }

        public int ArrivalTime { get; set; }
        public int PMTLocation { get; set; }
    }
    public class Page
    {
        public int PageNumber { get; set; }
        public int FrameNumber { get; set; }
        public int StatusBit { get; set; }
        public int ModifiedBit { get; set; }
        public int ReferenceBit { get; set; }
        public int SwappedOutBit { get; set; }
        public int DiskLocation { get; set; }
    }

    public class Frame
    {
        public int FrameNumber { get; set; }
        public bool IsOccupied { get; set; }
        public int JobNumber { get; set; }
        public int PageNumber { get; set; }
        
        public DateTime TimeLoaded { get; set; }
    }
}
