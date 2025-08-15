using System.Text.Json;

namespace Inventory_Records
{
    // Marker Interface
    public interface IInventoryEntity
    {
        int Id { get; }
    }

    // Immutable Inventory Record
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;

    // Generic Inventory Logger
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log = new();
        private readonly string _filePath;

        public InventoryLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item)
        {
            _log.Add(item);
        }

        public List<T> GetAll()
        {
            return new List<T>(_log);
        }

        public void SaveToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath))
                {
                    string json = JsonSerializer.Serialize(_log);
                    writer.Write(json);
                }
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("File does not exist. No data loaded.");
                    return;
                }

                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string json = reader.ReadToEnd();
                    var items = JsonSerializer.Deserialize<List<T>>(json);
                    if (items != null)
                        _log = items;
                }
                Console.WriteLine("Data loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
    }

    // Integration Layer
    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string filePath)
        {
            _logger = new InventoryLogger<InventoryItem>(filePath);
        }

        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(1, "Laptop", 5, DateTime.Now));
            _logger.Add(new InventoryItem(2, "Mouse", 20, DateTime.Now));
            _logger.Add(new InventoryItem(3, "Keyboard", 15, DateTime.Now));
            _logger.Add(new InventoryItem(4, "Monitor", 8, DateTime.Now));
            _logger.Add(new InventoryItem(5, "USB Drive", 50, DateTime.Now));
        }

        public void SaveData()
        {
            _logger.SaveToFile();
        }

        public void LoadData()
        {
            _logger.LoadFromFile();
        }

        public void PrintAllItems()
        {
            var items = _logger.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id}: {item.Name} - Qty: {item.Quantity}, Added: {item.DateAdded}");
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "inventory.json";

            // Create instance of InventoryApp
            InventoryApp app = new InventoryApp(filePath);

            // Seed and save data
            app.SeedSampleData();
            app.SaveData();

            // Simulate new session
            Console.WriteLine("\n--- Simulating new session ---\n");
            app = new InventoryApp(filePath);

            // Load and print data
            app.LoadData();
            app.PrintAllItems();
        }
    }
}
