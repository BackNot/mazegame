using RoomMaze.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze
{
    class Room : IRoom
    {
        public Room(string number, ICollection<Item> Items = null, bool isLocked = false, bool hasMonster = false, bool isFinalRoom = false)
        {
            this.Number = number;
            this.IsLocked = isLocked;
            this.HasMonster = hasMonster;
            this.IsFinalRoom = isFinalRoom;
            if (Items != null)
                this.AvaibleItems.AddRange(Items);
        }

        public List<Item> AvaibleItems { get; set; } = new List<Item>();
        public string Number { get; set; }
        public bool IsLocked { get; set; }
        public bool HasMonster { get; set; }
        public bool IsFinalRoom { get; set; }

        /* Check for the chosen item in the room. If it is there assign it to avaibleItem variable
         and return true. If it is not there return false. */
        public bool IsItemInRoom(string item, IRoom room, out Item avaibleItem)
        {
            var roomItem = room?.AvaibleItems?.Where(roomitems => roomitems.Name.ToLower() == item.ToLower()).FirstOrDefault();
            if (roomItem == null)
            {
                avaibleItem = null;
                return false;
            }
            avaibleItem = roomItem;
            return true;
        }

        // When user drops an item this method is called, so the item can be in the room.
        public void AddItem(Item item)
        {
            AvaibleItems.Add(item);
        }

        // When user picks an item this method is called.
        public void RemoveItem(Item item)
        {
            AvaibleItems.Remove(item);
        }
    }
}
