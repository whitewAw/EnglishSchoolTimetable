using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.model
{
    public class openingHourBL
    {
        public int Id { get; set; }
        public TimeSpan open { get; set; }
        public TimeSpan close { get; set; }
    }
}
