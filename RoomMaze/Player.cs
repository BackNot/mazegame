using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomMaze.Interfaces;
using RoomMaze.Items;

namespace RoomMaze
{
    class Player : IHuman
    {
        private string name;

        public Player(string name, IEnterable currentLevel)
        {
            this.CurrentMaze = currentLevel; 
            this.Name = name;
            this.CurrentLocation = CurrentMaze.Rooms[0, 0];
        }

        public int Health { get; set; } = 100; // Default value
        public IBackPack Bag { get; set; } = new BackPack();
        public IEnterable CurrentMaze { get; set; } // Current level (maze , building , roads , etc.)
        public IRoom CurrentLocation { get; set; } // Current location in the above instance.

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length <= 0) throw new ArgumentOutOfRangeException("Name is invalid");
                name = value;
            }
        }

        // Try to pick item from the room.
        public void PickItem(string item)
        {
            if (CurrentLocation.IsItemInRoom(item, CurrentLocation, out Item chosenItem))
            {
                if (Bag.Add(chosenItem))
                {
                    CurrentLocation.RemoveItem(chosenItem);
                    Bag.Show();
                }
            }
            else
            {
                Console.WriteLine("This item is not in the room. Look closely.");
            }
        }

        public void DropItem(string item)
        {
            var chosenItem = Bag.Get(item);
            if (chosenItem == null)
            {
                Console.WriteLine("You don't have this item.");
            }
            else
            {
                Bag.Remove(chosenItem);
                CurrentLocation.AddItem(chosenItem);
                Bag.Show();
            }
        }

        // This method will handle the room traversal. Find the chosen room, check it to 
        // see if it's close to the current. Check for locks, monsters, is it final, etc.
        // At last remove 5 health from the user.
        public void GoRoom(string room)
        {
            Room desiredLocation = (Room) CurrentMaze.GetRoom(room);
            if (desiredLocation != null)
            {
                if (CurrentMaze.IsNeighbor(desiredLocation, (Room) CurrentLocation))
                {
                    if (desiredLocation.IsLocked == true)
                    {
                        Console.WriteLine("--------------LOCKED----------------");
                        Console.WriteLine("Room is locked. You need a key to enter.");
                        Console.WriteLine("--------------LOCKED----------------");
                        return;
                    }
                    this.Health -= 5; 
                    if (this.Health <= 0)
                    {
                        this.Die();
                        return;
                    }
                    CurrentLocation = desiredLocation;
                    if (CurrentLocation.HasMonster)
                    {
                        this.Fight();
                    }
                    this.ShowLocation();

                    if(CurrentLocation.IsFinalRoom) // Check for potential win.
                    {
                        CurrentMaze.LevelPassed();
                    }
                    return;
                }
                else
                {
                    Console.WriteLine("You can't walk through walls.");
                    return;
                }
            }
            Console.WriteLine("Invalid location.");
        }

        // Print the maze to see where user is. Also print the items inside the room.
        public void ShowLocation()
        {
            ICollection<Item> itemsInRoom = null;
            Console.Write(Environment.NewLine);
            Console.WriteLine($"You are X. Your health is {this.Health} / 100");
            Console.Write(Environment.NewLine);
            int rowLength = CurrentMaze.Rooms.GetLength(0);
            int colLength = CurrentMaze.Rooms.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (CurrentMaze.Rooms[i, j].Number == CurrentLocation.Number)
                    {
                        Console.Write("X \t");
                        itemsInRoom = CurrentMaze.Rooms[i, j].AvaibleItems; // get items in the room
                    }
                    else Console.Write($"{CurrentMaze.Rooms[i, j].Number} \t");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            if (itemsInRoom.Count > 0)
            {
                Console.WriteLine("-----------ITEMS IN ROOM------------");
                foreach (Item item in itemsInRoom)
                {
                    Console.Write($"{item.Name} with weight of {item.Weight}");
                    Console.Write(Environment.NewLine);

                }
                Console.WriteLine("-----------ITEMS IN ROOM------------");
            }
            else
            {
                Console.WriteLine("-----------ITEMS IN ROOM------------");
                Console.WriteLine("No items in this room.");
                Console.WriteLine("-----------ITEMS IN ROOM------------");
            }
        }

        public void Die()
        {
            Console.WriteLine("--------------GAME OVER----------------");
            Console.WriteLine("Whoof...Sorry , but you are dead.");
            Console.ReadKey();
            Console.Write(Environment.NewLine);
            Console.WriteLine("GAME OVER");
            Console.ReadKey();
            Environment.Exit(0);
        }

        // This method will fight monsters. See if user has weapon. If he has the fight will be easier
        // and he will lose less health points. If not he will lose more.
        public void Fight()
        {
            if (this.Bag.Count() > 0)
            { 
                foreach (var item in this.Bag)
                {
                    if (item is Weapon)
                    {
                        CurrentLocation.HasMonster = false;
                        this.Health -= 10;
                        if (this.Health <= 0)
                        {
                            this.Die();
                            return;
                        }
                        Console.WriteLine("----------------FIGHT-----------------");
                        Console.WriteLine($"You killed a monster with {item.Name} ! Your health is now {this.Health} /100");
                        Console.WriteLine("----------------FIGHT-----------------");
                        return;
                    }
                }
            }
            // User don't have weapon.
            CurrentLocation.HasMonster = false;
            this.Health -= 30;
            if (this.Health <= 0)
            {
                this.Die();
                return;
            }
            Console.WriteLine("----------------FIGHT-----------------");
            Console.WriteLine($"You killed the monster with hands ! Your health is now {this.Health}");
            Console.WriteLine("----------------FIGHT-----------------");
        }

        // This method will handle the eating. If user has food he can restore some health points by eating.
        public void Eat()
        {
            if (this.Bag.Count() > 0)
            {
                foreach (var item in this.Bag)
                {
                    if (item is Food)
                    {
                        this.Bag.Remove(item);
                        this.Health += 20;
                        Console.WriteLine($"You ate {item.Name}. It was very delicious. Your health is now {this.Health} ");
                        return;
                    }
                }
            }
            Console.WriteLine("You have nothing to eat.");
        }

        // Try to unlock the chosen room. User needs a key.
        public void Unlock(string roomName)
        {
            var chosenRoom = CurrentMaze.GetRoom(roomName);
            if (chosenRoom == null)
            {
                Console.WriteLine("No such room");
                return;
            }
            if (!CurrentMaze.IsNeighbor(chosenRoom, this.CurrentLocation))
            {
                Console.WriteLine("You can't walk through walls");
            }
            else
            {
                if (chosenRoom.IsLocked)
                {
                    foreach (var item in this.Bag)
                    {
                        if (item is ISpecial)
                        {
                            chosenRoom.IsLocked = false;
                            this.Bag.Remove(item);
                            Console.WriteLine("--------------UNLOCKED----------------");
                            Console.WriteLine("Room unlocked.");
                            Console.WriteLine("--------------UNLOCKED----------------");
                            return;
                        }
                    }
                    Console.WriteLine("You don't have a key.");
                }
                else
                {
                    Console.WriteLine("Room is not locked.");
                }
            }
        }
    }
}
