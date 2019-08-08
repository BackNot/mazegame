using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze
{ 
    // Base class for all items.
    abstract class Item
    {
        public abstract double Weight { get; set; }
        public abstract string Name { get; set; }
    }
}
