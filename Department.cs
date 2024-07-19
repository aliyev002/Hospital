using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_END_
{
    public class Department
    {
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }

        public Department(string name, List<Doctor> doctors)
        {
            Name = name;
            Doctors = doctors;
        }
    }
}
