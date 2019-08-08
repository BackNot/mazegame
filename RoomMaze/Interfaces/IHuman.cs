using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze.Interfaces
{
    interface IHuman
    {
        string Name { get; set; }
        int Health { get; set; }
        IRoom CurrentLocation { get; set; } 
        IBackPack Bag { get; set; }
        void PickItem(string item);
        void DropItem(string item);
    }
}
