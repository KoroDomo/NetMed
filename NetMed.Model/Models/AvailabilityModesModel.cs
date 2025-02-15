using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMed.Model.Models
{
    public class AvailabilityModesModel
    {
        public int Id { get; set; }
        public string AvailabilityModeName { get; set; }

        public DateTime DateOfVisit { get; set; }
        public bool IsActive { get; set; }

    }
}
