using System.Runtime.ConstrainedExecution;

namespace Grading_System
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Score { get; set; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        public string GetGrade(int score)
        {
            if (Score >= 80) return "A";
            if (Score >= 70) return "B";
            if (Score >= 60) return "C";
            if (Score >= 50) return "D";
            return "F";
           
        }
    }

    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }

    public class MissingFieldException : Exception
    {
        public MissingFieldException(string message) : base(message) { }
    }

    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath) 
        {
            var students = new List<Student>();
            using (StreamReader reader = new StreamReader(inputFilePath)) 
            {
                string? line;
                while ((line = reader.ReadLine()) != null) 
                {

                    try
                    {
                        // Split line by comma
                        string[] parts = line.Split(',');

                        if (parts.Length != 2)
                        {
                            Console.WriteLine($"Invalid line format");
                            continue;
                        }

                        string name = parts[0].Trim();
                        string scoreStr = parts[1].Trim();

                        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(scoreStr))
                        {
                            throw new MissingFieldException($"Fields {nameof(name)}, and {nameof(scoreStr)} nust not be empty.");
                        }

                        if (!int.TryParse(scoreStr, out int score))
                        {
                            throw new InvalidScoreFormatException($"Invalid score value: {scoreStr}");
                        }
                        students.Add(new Student(students.Count() + 1, name, score));

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.Message}");
                    }
                }
            }
            return students;

        }

        public void WriteReportToFile(List<Student> students, string outPutFilePath) 
        {
            using (StreamWriter writer = new StreamWriter(outPutFilePath)) 
            {
                foreach (Student student in students)
                {
                    string summary = ($"{student.FullName} (ID: {student.Id}): score = {student.Score}, Grade = {student.GetGrade(student.Score)}");
                    writer.WriteLine(summary);
                }
            }
        }
    }
    internal class Grade_System
    {
        static void Main(string[] args)
        {
            // absolute Path: file at (<ProjectFolder>\bin\Debug\netX.0\)

            string inputFilePath = "students.txt";
            string outputFilePath = "report.txt";

            try
            {
                // Create sample student file if it doesn't exist
                if (!File.Exists(inputFilePath))
                {
                    string[] sampleStudents =
                    {
                    "John Doe,85",
                    "Jane Smith,92",
                    "Michael Johnson,78",
                    "Emily Davis,88",
                    "Daniel Brown,95",
                    "Sarah Wilson,73",
                    "David Lee,80",
                    "Laura Clark,90",
                    "James White,65",
                    "Olivia Martin,84"
                };

                    File.WriteAllLines(inputFilePath, sampleStudents);
                    Console.WriteLine($"Sample student file created: {inputFilePath}");
                }

                StudentResultProcessor processor = new StudentResultProcessor();

                // Read students from file
                List<Student> students = processor.ReadStudentsFromFile(inputFilePath);

                // Write report to output file
                processor.WriteReportToFile(students, outputFilePath);

                Console.WriteLine($"Report successfully written to {outputFilePath}");
            }
            catch (FileNotFoundException fnfEx)
            {
                Console.WriteLine($"File not found: {fnfEx.Message}");
            }
            catch (InvalidScoreFormatException scoreEx)
            {
                Console.WriteLine($"Invalid score format: {scoreEx.Message}");
            }
            catch (MissingFieldException fieldEx)
            {
                Console.WriteLine($"Missing field: {fieldEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
