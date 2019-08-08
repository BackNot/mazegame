using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze.Interfaces
{
    interface IRoom
    {
        List<Item> AvaibleItems { get; set; }
        string Number { get; set; }
        bool IsLocked { get; set; }
        bool HasMonster { get; set; }
        bool IsFinalRoom { get; set; }
        bool IsItemInRoom(string item, IRoom room, out Item avaibleItem);
        void AddItem(Item item);
        void RemoveItem(Item item);
    }
}
