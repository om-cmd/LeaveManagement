using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class CalanderViewModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Description { get; set; }
        public string Region { get; set; }
    }
}
