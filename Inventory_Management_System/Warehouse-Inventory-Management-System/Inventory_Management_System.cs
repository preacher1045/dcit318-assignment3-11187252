using System.Security.Cryptography.X509Certificates;

namespace Warehouse_Inventory_Management_System
{
    internal class Inventory_Management_System
    {
        public interface IInventoryItem
        {
            int Id { get; }
            string Name { get; }
            int Quantity { get; set; }
        }

        public class ElectricalItem : IInventoryItem
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public int Quantity { get; set; }
            public string Brand { get; set;}
            public int WarrantyMonths { get; set;}


            public ElectricalItem(int id, string name, int quantity, string brand, int warrantyMonth) 
            {
                Id = id;
                Name = name;
                Quantity = quantity;
                Brand = brand;
                WarrantyMonths = warrantyMonth;
            }
        }
        public class GroceryItem : IInventoryItem 
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public int Quantity { get; set; }
            public DateTime ExpiryDate { get; set; }


            public GroceryItem(int id, string name, int quantity, DateTime expiryDate) 
            {
                Id = id;
                Name = name;
                Quantity = quantity;
                ExpiryDate = expiryDate;
                
            }
        }

        public class DuplicateItemException : Exception
        {
            public DuplicateItemException(string message) : base(message) { }
        }

        public class ItemNotFoundException : Exception 
        {
            public ItemNotFoundException(string message) : base(message) { }
        }
        public class InvalidQuantityException : Exception 
        {
            public InvalidQuantityException(string message) : base(message) { }
        }



        public class InventoryRepository<T> where T: IInventoryItem 
        {
            private Dictionary<int, T> _items = new Dictionary<int, T>();


            public void AddItem(T item) 
            {
                if (_items.ContainsKey(item.Id)) 
                {
                    throw new DuplicateItemException($"Item with ID {item.Id} already exists.");
                }
                _items[item.Id] = item;
                
            }

            public T GetItemById (int id) 
            {
               if ( _items.TryGetValue(id, out T item)) 
               {
                    return item;
               }
               else
               {
                    throw new ItemNotFoundException($"Item with ID {id} was not found.");
               }
     
            }

            public void RemoveItem(int id) 
            {
                if (!_items.ContainsKey(id))
                {
                    throw new ItemNotFoundException($"Item with ID {id} was not found.");
                }
                _items.Remove(id);
               
            }

            public List<T> GetAllItems() 
            {
                return _items.Values.ToList();
            }

            public void UpdateQuantity(int id, int newQuantity)
            {
                if (!_items.ContainsKey(id))
                {
                    throw new ItemNotFoundException($"Item with ID {id} was not found.");
                }

                if (newQuantity <= 0) 
                {
                    throw new InvalidQuantityException($"Quantity must be a positive number (1 or more)");
                }

                GetItemById(id).Quantity = newQuantity;
            }
        }

        public class WareHouseManager 
        {
            private InventoryRepository<ElectricalItem> _electronics;
            private InventoryRepository<GroceryItem> _groceries;

            public void SeedData() 
            {
                _electronics = new InventoryRepository<ElectricalItem>();
                _groceries = new InventoryRepository<GroceryItem>();

                // Electricals
                _electronics.AddItem(new ElectricalItem(1, "Power drill", 2, "Bosch", 2));
                _electronics.AddItem(new ElectricalItem(2, "Microwave", 1, "Hisense", 3));
                _electronics.AddItem(new ElectricalItem(3, "Blender", 5, "Panasonic", 4));

                // Groceries
                _groceries.AddItem(new GroceryItem(1, "Chedder Cheese", 3, DateTime.Now));
                _groceries.AddItem(new GroceryItem(2, "Rice", 2, DateTime.Now));
                _groceries.AddItem(new GroceryItem(3, "Tomato", 16, DateTime.Now));
            }

            // Remember to use try....catch blocks in the methods below
            public void PrintAllItems<T>(InventoryRepository<T> repo) where T: IInventoryItem
            {
                foreach (var item in repo.GetAllItems())
                {
                    Console.WriteLine($"{item.Id}: {item.Name} (Qty: {item.Quantity})");
                }
            }

            public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T: IInventoryItem
            {
                if (quantity <= 0) 
                {
                    throw new InvalidQuantityException("Increase amount must be positive.");
                }

                var item = repo.GetItemById(id);
                item.Quantity += quantity;
            }

            public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
            {
                repo.RemoveItem(id);
            }

            public InventoryRepository<ElectricalItem> GetElectronicsRepo() => _electronics;
            public InventoryRepository<GroceryItem> GetGroceriesRepo() => _groceries;
        }



        static void Main(string[] args)
        {
            WareHouseManager manager = new WareHouseManager();
            manager.SeedData();

            Console.WriteLine("Grocery items: ");
            manager.PrintAllItems(manager.GetGroceriesRepo());

            Console.WriteLine("\n Elecrical items: ");
            manager.PrintAllItems(manager.GetElectronicsRepo());

            try
            {
                manager.RemoveItemById(manager.GetElectronicsRepo(), 89);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing item: {ex.Message}");
            }

            try
            {
                manager.IncreaseStock(manager.GetElectronicsRepo(), 1, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error increasing stock: {ex.Message}");
            }

            try
            {
                manager.GetElectronicsRepo().AddItem(new ElectricalItem(1, "Power drill", 2, "Bosch", 2));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding item: {ex.Message}");
            }



        }
    }
}
