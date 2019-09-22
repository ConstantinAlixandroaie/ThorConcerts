using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThorConcerts.ViewModels
{
    public class ConcertViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BandName { get; set; }
        public int? Capacity { get; set; }
    }
}
