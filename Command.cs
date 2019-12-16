using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    abstract class Command
    {
 
        public abstract LocationInfo ExecuteCommand(LocationInfo initialLocationInfo);

    }
}
