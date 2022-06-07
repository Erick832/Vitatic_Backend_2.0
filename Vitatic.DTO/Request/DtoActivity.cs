using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitatic.Entities;

namespace Vitatic.DTO.Request
{
    public class DtoActivity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime Date { get; set; }
        public int Minutes { get; set; }
        //public Progress Progress { get; set; }
    }
}
