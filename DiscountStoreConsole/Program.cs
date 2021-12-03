using DiscountStoreConsole.Entities;
using DiscountStoreConsole.Services;
using System;
using System.Collections.Generic;

namespace DiscountStoreConsole
{
    class Program
    {
        private static readonly Item _vase = new Item("Vase", 1.2, 0, 0);
        private static readonly Item _bigMug = new Item("Big Mug", 1, 1.5, 2);
        private static readonly Item _napkins = new Item("Napkins Pack", 0.45, 0.9, 3);
        private static readonly CartService cartService = new CartService();
        static void Main(string[] args)
        {
            var itemsList = new List<Item> {_vase, _bigMug, _napkins };
            

            Console.WriteLine("Available items with ids:");
            for (int i = 0; i<itemsList.Count; i++)
            {
                Console.WriteLine($"{i}: {itemsList[i].Name}");
            }
            Console.WriteLine("Enter first input");
            Console.WriteLine("Available commands: total, add, remove");


            var command = Console.ReadLine();

            while (command != null && !string.Equals(command.Trim(), "exit", StringComparison.InvariantCultureIgnoreCase))
            {

                switch (command)
                {
                    case "add":
                    {
                        AddItem(itemsList);
                    }
                        break;
                    case "remove":
                    {
                        RemoveItem(itemsList);
                    }
                        break;
                    case "total":
                    {
                        Console.WriteLine(cartService.ToString());
                    }
                        break;
                    default:
                        Console.WriteLine("Unknown command, available commands: total, add, remove");
                        break;
                }
                command = Console.ReadLine().Trim().ToLower();
            }
            

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }

        private static void AddItem(List<Item> itemsList)
        {
            Console.WriteLine("Type the id number of the item you want to add");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                if (id >= 0 && id < itemsList.Count)
                {
                    cartService.Add(itemsList[id]);
                    Console.WriteLine($"Added {itemsList[id].Name} to the list");

                    return;
                }

            }
            Console.WriteLine("could not parse input into item from the list");

        }

        private static void RemoveItem(List<Item> itemsList)
        {
            Console.WriteLine("Type the id number of the item you want to remove");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                if (id >= 0 && id < itemsList.Count)
                {
                    cartService.Remove(itemsList[id]);
                    Console.WriteLine($"Removed {itemsList[id].Name} from the list (or did nothing if it wasn't there");
                    return;
                }

            }
            Console.WriteLine("could not parse input into item from the list");

        }
    }
}
