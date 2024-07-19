using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_END_
{
    public class Doctor
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Experience { get; set; }

        public Doctor(string name, string surname, int experience)
        {
            Name = name;
            Surname = surname;
            Experience = experience;
        }
    }
}
