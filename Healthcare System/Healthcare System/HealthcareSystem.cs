namespace Healthcare_System
{
    internal class HealthcareSystem
    {
        //static Dictionary<int, List<Prescription>> patientPrescriptionList = new Dictionary<int, List<Prescription>>();
        public class Repository<T> 
        {
            private List<T> items = new List<T>();
            

            public void Add(T item) 
            {
                items.Add(item);
            }

            public List<T> GetAll() {  return items; }

            public T? GetById(Func<T, bool> predicate) { return items.FirstOrDefault(predicate); }

            public bool Remove(Func<T, bool> predicate) 
            {
                // Get first item in the list
                var item = items.FirstOrDefault(predicate);

                // Check if list is empty/null
                if (item != null) 
                {
                    items.Remove(item);
                    return true;
                }
                return false;
            }
        }

        public class Patient 
        {
            public int Id {  get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }

            public Patient(int id, string name, int age, string gender) 
            {
                Id = id;
                Name = name;
                Age = age;
                Gender = gender;
            }
        }

        public class Prescription
        {
            public int Id { get; set; }
            public int PatientId { get; set; }
            public string MedicationName { get; set; }
            public DateTime DateIssued { get; set; }

            public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
            {
                Id = id;
                PatientId = patientId;
                MedicationName = medicationName;
                DateIssued = dateIssued;
            }
        }

        //public static List<Prescription> GetPrescriptionsByPatientId(int patientId) 
        //{
        //    if (patientPrescriptionList.ContainsKey(patientId) 
        //    {
        //        return patientPrescriptionList[patientId];
        //    }
        //    return new List<Prescription>();
        //}


        public class HealthSystemApp
        {
            private Repository<Patient> _patientRepo;
            private Repository<Prescription> _prescriptionRepo;
            private Dictionary<int, List<Prescription>> _prescriptionMap;

            public HealthSystemApp()
            {
                _patientRepo = new Repository<Patient>();
                _prescriptionRepo = new Repository<Prescription>();
                _prescriptionMap = new Dictionary<int, List<Prescription>>();
            }

            public void SeedData()
            {
                //____Patients____
                _patientRepo.Add(new Patient(101, "Alice", 30, "Female"));
                _patientRepo.Add(new Patient(102, "John", 20, "Male"));
                _patientRepo.Add(new Patient(103, "Dorothy", 17, "Female"));

                //____Prescription__

                _prescriptionRepo.Add(new Prescription(1, 101, "Paracetamol", DateTime.Now));
                _prescriptionRepo.Add(new Prescription(2, 102, "Ibuprofen", DateTime.Now));
                _prescriptionRepo.Add(new Prescription(3, 103, "Amoxacilin", DateTime.Now));
                

            }

            public List<Prescription> GetPrescriptionsByPatientId(int patientId) 
            {
                if (_prescriptionMap.ContainsKey(patientId)) 
                {
                    return _prescriptionMap[patientId];
                }
                return new List<Prescription>();
            }

            public void BuildPrescriptionMap()
            {
                _prescriptionMap.Clear();

                foreach (var prescription in _prescriptionRepo.GetAll())
                {
                    if (!_prescriptionMap.ContainsKey(prescription.PatientId))
                    {
                        _prescriptionMap[prescription.PatientId] = new List<Prescription>();
                    }

                    _prescriptionMap[prescription.PatientId].Add(prescription);
                }
            }

            public void PrintAllPatients() 
            {
                foreach (var patients in _patientRepo.GetAll()) 
                {
                    Console.WriteLine($"{patients.Id} - {patients.Name}, Age {patients.Age}, Gender {patients.Gender}\n");
                }
            }
            public void PrintAllPrescriptions(int patientId)
            {
                if (_prescriptionMap.ContainsKey(patientId))
                {
                    foreach (var p in _prescriptionMap[patientId])
                    {
                        Console.WriteLine($"{p.Id} - {p.MedicationName}, Issued: {p.DateIssued}\n");
                    }
                }
                else
                {
                    Console.WriteLine($"No prescriptions found for patient ID {patientId}");
                }
            }

        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            HealthSystemApp app = new HealthSystemApp();

            app.SeedData();
            app.BuildPrescriptionMap();

            Console.WriteLine("Patients:");
            app.PrintAllPatients();

            Console.WriteLine("\nPrescriptions for patient 101:");
            app.PrintAllPrescriptions(101);
        }
    }
}
