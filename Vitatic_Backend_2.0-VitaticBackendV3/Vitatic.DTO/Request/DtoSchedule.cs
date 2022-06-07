using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitatic.Entities;

namespace Vitatic.DTO.Request
{
    public class DtoSchedule
    {
        public ICollection<Activity> Activities { get; set; }
    }
}
