﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    public class ConsoleHandler
    {
        //this will allow unit testing
        public string GetUserInput() {

           return Console.ReadLine().ToUpper();


        }

        public void DisplayText(string text)
        {

            Console.WriteLine(text);

        }
    }
}
