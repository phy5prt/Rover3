using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    public static class ConsoleHandler
    {
        //this will allow unit testing
        public static string GetUserInput() {

            //Console.WriteLine();
            //Console.WriteLine("Please type and return: ");
            //Console.WriteLine();
            string userInput = Console.ReadLine().ToUpper();
            //Console.WriteLine();
            return userInput;
            

        }

        public static void DisplayText(string text)
        {

            Console.WriteLine(text);

        }
    }
}
