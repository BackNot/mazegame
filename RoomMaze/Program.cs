using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomMaze.Interfaces;

namespace RoomMaze
{ 
    class Program
    {
        static void ShowHelp()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("help - show list of command");
            Console.WriteLine("info - show information and rules");
            Console.WriteLine("location - show current location");
            Console.WriteLine("goto  - move to another room");
            Console.WriteLine("items - show what items you have in your backpack");
            Console.WriteLine("eat - eat something");
            Console.WriteLine("unlock - use your key to unlock a room");
            Console.WriteLine("pick - pick up item and put it in the backpack");
            Console.WriteLine("drop - remove the item from the backpack and put it in the room");
            Console.WriteLine("quit - exit game");
        }
        static void Information()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("---OVERVIEW---");
            Console.WriteLine("Maze has total of 49 rooms. You need to find the room where the princess is hiding. After that you win.");
            Console.WriteLine("There are monsters in some rooms , so be on the watch. There may be some items in each room to help you for your journey.");
            Console.WriteLine("Some rooms are locked and you need to find a key in order to enter.");
            Console.WriteLine("You have health bar , so the more you move the less health you have.You lose if your health is 0%.");
            Console.Write(Environment.NewLine);
            Console.WriteLine("---ITEMS---");
            Console.WriteLine("There are 3 types of items.");
            Console.WriteLine("Weapons - This will help you fight monsters. You will lose a lot less when fighting them");
            Console.WriteLine("Food - This will help you restore your health bar.");
            Console.WriteLine("Keys - This will help you open doors that are locked.");
            Console.Write(Environment.NewLine);
            Console.WriteLine("May the force be with you.");
        }
        static void Main(string[] args)
        {
            // Note: We don't force a player to play in this maze only. We can play in another
            // space instead, but we need to inherit from IEnterable and pass it to the constructor.
            IEnterable maze = new Maze();
            Player user = new Player("root",maze);

            string command = string.Empty;
            while (command != "quit")
            {
                Console.Write(Environment.NewLine);
                Console.Write(Environment.NewLine);
                Console.WriteLine("Enter command:");
                command = Console.ReadLine();
                switch (command)
                {
                    case "info": Information(); break;
                    case "location": user.ShowLocation(); break;
                    case "goto":
                        {
                            Console.WriteLine("Enter Room Name: ");
                            string roomName = Console.ReadLine();
                            user.GoRoom(roomName);
                            break;
                        }
                    case "items": user.Bag.Show(); break;
                    case "eat": user.Eat(); break;
                    case "unlock":
                        {
                            Console.WriteLine("Enter room name to unlock:");
                            string roomName = Console.ReadLine();
                            user.Unlock(roomName);
                            break;
                        }
                    case "pick":
                        {
                            Console.WriteLine("Item name:");
                            string itemName = Console.ReadLine();
                            user.PickItem(itemName);
                            break;
                        }
                    case "drop":
                        {
                            Console.WriteLine("Item name:");
                            string itemName = Console.ReadLine();
                            user.DropItem(itemName);
                            break;
                        }
                    case "health":
                        {
                            Console.WriteLine($"{user.Health} / 100.00"); break;
                        }
                    case "quit":
                        {
                            Console.Write(Environment.NewLine);
                            Console.WriteLine("Thank you for playing. Press any key to exit."); break;
                        }
                    default:
                        ShowHelp(); break;
                }
            }
            Console.ReadKey();
        }
    }
}
