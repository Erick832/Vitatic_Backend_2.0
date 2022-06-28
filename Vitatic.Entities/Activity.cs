using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitatic.Entities
{
    public class Activity:EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }   
        public DateTime Date { get; set; }
        public int Minutes { get; set; } 
        //public int ProgressId { get; set; }
        public Progress Progress { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        
    }
}
