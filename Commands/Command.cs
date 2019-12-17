using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.Commands
{
    abstract class Command //Command Class
    {
 
        public abstract LocationInfo ExecuteCommand(LocationInfo initialLocationInfo);

    }
}
