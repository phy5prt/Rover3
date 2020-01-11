using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    public static class ConsoleHandler
    {
        //this will allow unit testing
        public static string GetUserInput() {

           return Console.ReadLine().ToUpper();


        }

        public static void DisplayText(string text)
        {

            Console.WriteLine(text);

        }
    }
}
