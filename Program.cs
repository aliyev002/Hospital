using Hospital_END_;
using Newtonsoft.Json;
using System.Text.Json;

class Program
{
    private static string filePath = "appointments.json";

    public static List<Department> InitializeDepartments()
    {
        var pediatriyaDoctors = new List<Doctor>
            {
                new Doctor("Aysel", "Mammadova", 10),
                new Doctor("Elchin", "Aliyev", 5)
            };
        var travmatologiyaDoctors = new List<Doctor>
            {
                new Doctor("Hesen", "Ibrahimov", 15),
                new Doctor("Rashad", "Huseynov", 8)
            };
        var stomatologiyaDoctors = new List<Doctor>
            {
                new Doctor("Leyla", "Karimova", 12),
                new Doctor("Elsever", "Rehimov", 9)
            };

        var departments = new List<Department>
            {
                new Department("Pediatriya", pediatriyaDoctors),
                new Department("Travmatologiya", travmatologiyaDoctors),
                new Department("Stomatologiya", stomatologiyaDoctors)
            };

        return departments;
    }

    public static User GetUserInformation()
    {
        try
        {
            Console.Write("Ad: ");
            string name = Console.ReadLine();

            Console.Write("Soyad: ");
            string surname = Console.ReadLine();

            string email;
            while (true)
            {
                Console.Write("Email (gmail.com ile bitmelidi): ");
                email = Console.ReadLine();
                if (email.EndsWith("@gmail.com"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Zehmet olmasa yeniden Gmail adresi girin.");
                }
            }

            Console.Write("Telefon: ");
            string phone = Console.ReadLine();

            return new User(name, surname, email, phone);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
            throw;
        }
    }

    public static Department SelectDepartment(List<Department> departments)
    {
        try
        {
            while (true)
            {
                Console.WriteLine("Shobe sechin (evvelki menyuya qayitmaq uchun 0 daxil edin):");
                for (int i = 0; i < departments.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {departments[i].Name}");
                }

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 0)
                    {
                        return null;
                    }
                    else if (choice > 0 && choice <= departments.Count)
                    {
                        return departments[choice - 1];
                    }
                }

                Console.WriteLine("Yanlish sechim. Yeniden cehd edin.");
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Format xetasi: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
            return null;
        }
    }

    public static Doctor SelectDoctor(Department department)
    {
        try
        {
            while (true)
            {
                Console.WriteLine($"{department.Name} shobesinden hekim sechin (evvelki menyuya qayitmaq uchun 0 daxil edin):");
                for (int i = 0; i < department.Doctors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {department.Doctors[i].Name} {department.Doctors[i].Surname} {department.Doctors[i].Experience} il tecrubeli");
                }

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 0) return null;
                    if (choice > 0 && choice <= department.Doctors.Count)
                        return department.Doctors[choice - 1];
                }

                Console.WriteLine("Yanlish secim. Yeniden cehd edin.");
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Format xetası: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
            return null;
        }
    }

    public static DateTime SelectDate()
    {
        try
        {
            while (true)
            {
                Console.Write("Tarixi daxil edin (YYYY-MM-DD) (evvelki menyuya qayitmaq uchun 0 daxil edin): ");
                string dateString = Console.ReadLine();
                if (dateString == "0") return DateTime.MinValue;

                DateTime date;
                if (DateTime.TryParse(dateString, out date))
                    return date;

                Console.WriteLine("Yanlish tarix formati. Yenidən cehd edin.");
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Format xetasi: {ex.Message}");
            return DateTime.MinValue;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
            return DateTime.MinValue;
        }
    }

    public static Appointment.AppointmentTime? SelectTime()
    {
        try
        {
            while (true)
            {
                Console.WriteLine("Qebul saatini sechin (evvelki menyuya qayitmaq uchun 0 daxil edin):");
                Console.WriteLine("1. 09:00-12:00");
                Console.WriteLine("2. 15:00-18:00");
                Console.WriteLine("3. 19:00-22:00");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 0) return null;
                    if (choice > 0 && choice <= 3)
                        return (Appointment.AppointmentTime)choice;
                }

                Console.WriteLine("Yanlish sechim. Yeniden cehd edin.");
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Format xetasi: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
            return null;
        }
    }

    public static void SaveAppointments(List<Appointment> appointments)
    {
        try
        {
            string json = JsonConvert.SerializeObject(appointments, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Fayla yazma xetasi: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Girish xetasi: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
        }
    }

    public static List<Appointment> LoadAppointments()
    {
        try
        {
            if (!File.Exists(filePath))
                return new List<Appointment>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Appointment>>(json);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Fayldan oxuma xetasi: {ex.Message}");
            return new List<Appointment>();
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Girish xetasi: {ex.Message}");
            return new List<Appointment>();
        }
        catch (Newtonsoft.Json.JsonException ex)
        {
            Console.WriteLine($"JSON xetasi: {ex.Message}");
            return new List<Appointment>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
            return new List<Appointment>();
        }
    }

    static void CreateAppointment(List<Department> departments, List<Appointment> appointments)
    {
        User user = GetUserInformation();
        Department department;
        Doctor doctor;
        DateTime date;
        Appointment.AppointmentTime? appointmentTime;

        while (true)
        {
            department = SelectDepartment(departments);
            if (department == null) return;

            doctor = SelectDoctor(department);
            if (doctor == null) return;

            date = SelectDate();
            if (date == DateTime.MinValue) return;

            appointmentTime = SelectTime();
            if (appointmentTime == null) return;

            if (IsAppointmentConflict(appointments, doctor, date, appointmentTime.Value))
            {
                Console.WriteLine("Bu tarixde ve saatda artiq rezervasiya movcuddur. Zehmet olmasa bashqa bir vaxt sechin.");
                continue;
            }

            Appointment appointment = new Appointment(user, doctor, department, date, appointmentTime.Value);
            appointments.Add(appointment);
            SaveAppointments(appointments);

            string timeRange;

            if (appointment.Time == Appointment.AppointmentTime.Morning)
            {
                timeRange = "09:00-12:00";
            }
            else if (appointment.Time == Appointment.AppointmentTime.Noon)
            {
                timeRange = "15:00-18:00";
            }
            else if (appointment.Time == Appointment.AppointmentTime.Afternoon)
            {
                timeRange = "19:00-22:00";
            }
            else
            {
                timeRange = "Unknown";
            }

            Console.WriteLine($"Teshekkur edirik {appointment.User.Name} {appointment.User.Surname}, siz {appointment.Date.ToString("yyyy-MM-dd")} tarixinde saat {timeRange}-da {appointment.Doctor.Name} {appointment.Doctor.Surname} hekimin qebuluna yazildiniz.");
            return;
        }
    }

    static void ShowAppointments(List<Appointment> appointments)
    {
        try
        {
            if (appointments.Count == 0)
            {
                Console.WriteLine("Hech bir qebul yoxdur.");
                return;
            }

            foreach (var appointment in appointments)
            {
                string timeRange;
                switch (appointment.Time)
                {
                    case Appointment.AppointmentTime.Morning:
                        timeRange = "09:00-12:00";
                        break;
                    case Appointment.AppointmentTime.Noon:
                        timeRange = "15:00-18:00";
                        break;
                    case Appointment.AppointmentTime.Afternoon:
                        timeRange = "19:00-22:00";
                        break;
                    default:
                        timeRange = "Bilinmir";
                        break;
                }

                Console.WriteLine($"Ad: {appointment.User.Name}, Soyad: {appointment.User.Surname}, Email: {appointment.User.Email}, Telefon: {appointment.User.Phone}");
                Console.WriteLine($"Hekim: {appointment.Doctor.Name} {appointment.Doctor.Surname}, Shobe: {appointment.Department.Name}");
                Console.WriteLine($"Tarix: {appointment.Date.ToString("yyyy-MM-dd")}, Saat: {timeRange}");
                Console.WriteLine(" ");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
        }
    }

    static bool IsAppointmentConflict(List<Appointment> appointments, Doctor doctor, DateTime date, Appointment.AppointmentTime time)//yoxlayir gorsun ki  bu tarix ve saatda appointment varmi
    {
        try
        {
            foreach (var appointment in appointments)
            {
                if (appointment.Doctor.Name == doctor.Name &&
                    appointment.Doctor.Surname == doctor.Surname &&
                    appointment.Date.Date == date.Date &&
                    appointment.Time == time)
                {
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
            return false;
        }
    }

    static void Main(string[] args)
    {
        try
        {
            var departments = InitializeDepartments();
            var appointments = LoadAppointments();

            while (true)
            {
                Console.WriteLine("Menyu:");
                Console.WriteLine("1. Yeni qebul yarat");
                Console.WriteLine("2. Qebullari goster");
                Console.WriteLine("0. Chixish");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateAppointment(departments, appointments);
                        break;
                    case 2:
                        ShowAppointments(appointments);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Yanlish sechim. Yeniden cehd edin.");
                        break;
                }
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Format xetasi: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xeta bash verdi: {ex.Message}");
        }
    }
}

