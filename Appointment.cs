using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_END_
{
    public class Appointment
    {
        public enum AppointmentTime
        {
            Morning = 1,    // 09:00-12:00
            Noon = 2,       // 15:00-18:00
            Afternoon = 3   // 19:00-22:00
        }

        public User User { get; set; }
        public Doctor Doctor { get; set; }
        public Department Department { get; set; }
        public DateTime Date { get; set; }
        public AppointmentTime Time { get; set; }

        public Appointment(User user, Doctor doctor, Department department, DateTime date, AppointmentTime time)
        {
            User = user;
            Doctor = doctor;
            Department = department;
            Date = date;
            Time = time;
        }
    }
}
