using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitatic.Entities
{
    public class Progress : EntityBase
    {
        public int Repetitions { get; set; }
        public int Points { get; set; }
        public int ScheduleId { get; set; }
        public Activity Activity { get; set; }
    }
}
