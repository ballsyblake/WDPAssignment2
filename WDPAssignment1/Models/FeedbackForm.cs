using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WDPAssignment1.Models
{
    public class FeedbackForm
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string Heading { get; set; }
        public string Technology { get; set; }
        public string Rating { get; set; }
        public string Feedback { get; set; }

    }
}
