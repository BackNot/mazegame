using RoomMaze.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMaze
{
    class Maze : IEnterable
    {
        public Maze()
        {
            SeedMaze();
        }

        public IRoom[,] Rooms { get; set; }

        public IRoom GetRoom(string roomNumber)
        {
            var room = (from IRoom rm in Rooms
                        where rm.Number == roomNumber
                        select rm).SingleOrDefault();
            return room;
        }

        public void LevelPassed()
        {
            // A user has found the final room => game over.
            Console.Clear();
            Console.WriteLine("-----------CONGRATZ------------");
            Console.WriteLine("-----------YOU WON------------");
            Console.WriteLine("-----------YOU WON------------");
            Console.WriteLine("-----------YOU WON------------");
            Console.WriteLine("-----------CONGRATZ------------");

            Console.ReadKey();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public (int, int) GetRoomIndexes(IRoom[,] matrix, IRoom value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return (x, y);
                }
            }

            return (-1, -1);
        }

        // Check if attempted room is close to the current (if true => relocation is allowed). 
        public bool IsNeighbor(IRoom destination, IRoom current)
        {
            (int x, int y) = GetRoomIndexes(this.Rooms, current);
            if (x == -1) return false;
            IRoom leftNeighbor = null, upNeighbor = null, downNeighbor = null, rightNeighbor = null;

            /* Note: The catch blocks below only catch the exception but don't handle it.
             * Worst case scenario: our neighbor variable will be null , but we check 
             * it against the desired room to look for a match. So we will continue with
             * the normal flow if index is out of range. */
            try
            {
                leftNeighbor = Rooms[x, y - 1];
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                rightNeighbor = Rooms[x, y + 1];
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                upNeighbor = Rooms[x - 1, y];
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                downNeighbor = Rooms[x + 1, y];
            }
            catch (IndexOutOfRangeException) { }

            // Check if requested room is next to the current.
            if ((destination == leftNeighbor) || (destination == upNeighbor)
                || (destination == downNeighbor) || (destination == rightNeighbor))
            {
                return true;
            }
            return false;
        }

        // Fill the maze with rooms, items, monsters, etc. 
        public void SeedMaze()
        {
            List<Item> AllItemSet1 = new List<Item>()
            {
                new KatanaSword(),new Banana(),new Key()
            };
            List<Item> AllItemSet2 = new List<Item>()
            {
                new Pistol(), new Cake()
            };
            List<Item> WeaponSet1 = new List<Item>()
            {
                new Sword()
            };
            List<Item> WeaponSet2 = new List<Item>()
            {
                new Shotgun()
            };
            List<Item> WeaponSet3 = new List<Item>()
            {
                new Pistol()
            };
            List<Item> FoodSet1 = new List<Item>()
            {
                new Cake(), new Banana()
            };
            List<Item> FoodSet2 = new List<Item>()
            {
                new Banana()
            };
            List<Item> KeySet = new List<Item>()
            {
                new Key()
            };

            // This is the maze itself. Hardcoded length. Total: 49 rooms
            Rooms = new Room[7, 7] {
                { new Room("r1"), new Room("r2",Items: AllItemSet1),new Room("r3",hasMonster: true),new Room("r4"),new Room("r5", isLocked: true), new Room("r6"), new Room("r7") },
                { new Room("r8",hasMonster: true), new Room("r9", Items:FoodSet2),new Room("r10"),new Room("r11"),new Room("r12",hasMonster: true),new Room("r13"), new Room("r14") },
                { new Room("r15"), new Room("r16"),new Room("r17", Items:FoodSet2),new Room("r18"),new Room("r19"),new Room("r20",isLocked: true), new Room("r21") },
                { new Room("r22"), new Room("r23"),new Room("r24", Items: WeaponSet1),new Room("r25"),new Room("r26"),new Room("r27",isLocked: true), new Room("r28") },
                { new Room("r29"), new Room("r30"),new Room("r31"),new Room("r32", Items:WeaponSet2),new Room("r33"),new Room("r34",isLocked: true), new Room("r35") },
                { new Room("r36"), new Room("r37"),new Room("r38"),new Room("r39", Items: WeaponSet3),new Room("r40"),new Room("r41",isLocked: true), new Room("r42") },
                 { new Room("r43"), new Room("r44"),new Room("r45"),new Room("r46", Items:AllItemSet1),new Room("r47"),new Room("r48",isLocked: true), new Room("r49",isLocked: true, isFinalRoom: true) },
            };


        }
    }
}
