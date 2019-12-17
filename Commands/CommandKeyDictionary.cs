using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.Commands
{
    static class CommandKeyDictionary
    {
        //should this be in commands folder and static or should it be 
        public static Dictionary commandKeys = new Dictionary<string, Command>() 
        {
            {"F", new MoveForward() },
            {"B", new MoveBackward() },
            {"N", new LookNorth() },
            {"E", new LookEast() },
            {"S", new LookSouth() },
            {"W", new LookWest() },

        }
        
        }
    }
}
