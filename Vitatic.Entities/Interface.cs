using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitatic.Entities
{
    public class Interface:EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Schedule Schedule { get; set; }
        public AdviceSection AdviceSection { get; set; }

    }
}
