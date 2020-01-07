using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.Commands
{
    abstract class Command //Command Class
    {
        public abstract string Key { get; }
        public abstract LocationInfo ExecuteCommand(LocationInfo initialLocationInfo);

    }
}
