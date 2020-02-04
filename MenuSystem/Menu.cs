using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MenuSystem
{
    public class Menu
    {
        private int _menuLevel;
        private const string MenuCommandExit = "X";
        private const string MenuCommandReturnToPrevious = "P";

        private Dictionary<string, MenuItem> _menuItemsDictionary = new Dictionary<string, MenuItem>();

        public Menu(int menuLevel = 0)
        {
            _menuLevel = menuLevel;
        }

        public string Title { get; set; }

        public Dictionary<string, MenuItem> MenuItemsDictionary
        {
            get => _menuItemsDictionary;
            set
            {
                _menuItemsDictionary = value;
                if (_menuLevel >= 2)
                {
                    _menuItemsDictionary.Add(MenuCommandReturnToPrevious, new MenuItem() {Title = "Back"});
                }

                if (_menuLevel >= 1)
                {
                    _menuItemsDictionary.Add(MenuCommandExit, new MenuItem() {Title = "Exit"});
                }
            }


        }
        
        public string Run()
        {
            var command = "";
            do
            {
                Console.WriteLine(Title);
                Console.WriteLine("========================");

                foreach (var menuItem in MenuItemsDictionary)
                {
                    Console.Write(menuItem.Key);
                    Console.Write(" ");
                    Console.WriteLine(menuItem.Value);
                }
                
                Console.WriteLine("----------");
                Console.Write(">");

                command = Console.ReadLine()?.Trim().ToUpper() ?? "";


                var returnCommand = "";

                if (MenuItemsDictionary.ContainsKey(command))
                {
                    var menuItem = MenuItemsDictionary[command];
                    if (menuItem.CommandToExecute != null)
                    {
                        returnCommand = menuItem.CommandToExecute(); // run the command 
                        break;
                    }
                }



                if (returnCommand == MenuCommandExit)
                {
                    command = MenuCommandExit;
                }

                



            } while (command != MenuCommandExit &&
                     command != MenuCommandReturnToPrevious);

            
            return command;
        }
        
        
    }
}