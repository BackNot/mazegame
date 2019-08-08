using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze.Interfaces
{
    interface IEnterable
    {
        IRoom[,] Rooms { get; set; }
        IRoom GetRoom(string roomNumber);
        (int, int) GetRoomIndexes(IRoom[,] matrix, IRoom value);
        bool IsNeighbor(IRoom destination, IRoom current);
        void LevelPassed();
        void SeedMaze();
    }
}
