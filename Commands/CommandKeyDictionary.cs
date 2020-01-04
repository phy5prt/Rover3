using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.Commands
{
    static class CommandKeyDictionary
    {
        //should this be in commands folder and static or should it be 
        //should it build itself
        //https://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class
        public static Dictionary<string, Command> commandKeys = new Dictionary<string, Command>()
        {
            {"F", new MoveForward() },
            {"B", new MoveBackward() },
            {"N", new LookNorth() },
            {"E", new LookEast() },
            {"S", new LookSouth() },
            {"W", new LookWest() }

        };
        
        
    }
}
