using RoomMaze.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze
{
    class BackPack : IBackPack, IEnumerable<Item>
    {
        private List<Item> Items = new List<Item>();

        // Default capacity
        public double Capacity { get; set; } = 50.00;

        // Implement IEnumerable interface
        public IEnumerator<Item> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Try to add an item to the bag.
        public bool Add(Item item)
        {
            if (Capacity < item.Weight)
            {
                Console.WriteLine("--------------BAG IS FULL----------------");
                Console.WriteLine("There is no space in the bag.");
                Console.WriteLine("--------------BAG IS FULL----------------");
                return false;
            }
            Items.Add(item);
            Capacity -= item.Weight;
            return true;
        }

        public void Remove(Item item)
        {
            Items.Remove(item);
            Capacity += item.Weight;
        }

        // Search for the 'item name' in the bag. Notice that it accepts item name as a string.
        public Item Get(string item)
        {
            var chosenItem = Items?.Where(avaibleItems => avaibleItems.Name.ToLower() == item.ToLower()).FirstOrDefault();
            return chosenItem;
        }

        // List everything in the bag.
        public void Show()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine($"Your backpack (You have {this.Capacity} free capacity ):");
            Console.WriteLine("--------------BACKPACK----------------");
            foreach (var item in Items)
            {
                Console.WriteLine($" Name: {item.Name} Weight: {item.Weight}");
            }
            if (Items.Count == 0) Console.WriteLine("You have no items.");
            Console.WriteLine("--------------BACKPACK----------------");

        }
    }
}
