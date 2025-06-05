using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{


    public class TranscribeData
    {
        public string jobName { get; set; }
        public string accountId { get; set; }
        public string status { get; set; }
        public Results results { get; set; }
    }

    public class Results
    {
        public Transcript[] transcripts { get; set; }
        public Item[] items { get; set; }
        public Audio_Segments[] audio_segments { get; set; }
    }

    public class Transcript
    {
        public string transcript { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public string type { get; set; }
        public Alternative[] alternatives { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
    }

    public class Alternative
    {
        public string confidence { get; set; }
        public string content { get; set; }
    }

    public class Audio_Segments
    {
        public int id { get; set; }
        public string transcript { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public int[] items { get; set; }
    }

}
