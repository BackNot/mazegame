using RoomMaze.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze
{
    class Key : Item, ISpecial
    {
        public override double Weight { get; set; } = 5.00;
        public override string Name { get; set; } = "Key";
    }
}
