using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitatic.Entities
{
    public class Schedule : EntityBase
    {
        public int InterfaceId { get; set; }
        public Interface Interface { get; set; }
       
    }
}
