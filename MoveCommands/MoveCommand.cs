using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands
{
    abstract class MoveCommand : IKeyboardKey //Command Class
    {
        public abstract string Key { get; }
        public abstract string KeyFunctionDescription { get; }
        public abstract LocationInfo ExecuteCommand(LocationInfo initialLocationInfo);

    }
}
