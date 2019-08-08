using RoomMaze.Interfaces;
using RoomMaze.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze
{
    class Sword : Weapon
    {
        public override double Weight { get; set; } = 20.00;
        public override string Name { get; set; } = "Sword";
    }
}
