using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze.Interfaces
{
    interface IBackPack : IEnumerable<Item>
    {
        bool Add(Item item);
        void Remove(Item item);
        Item Get(string item);
        void Show();
        double Capacity { get; set; }
    }
}
