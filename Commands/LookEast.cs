using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.Commands
{
    class LookEast : Command
    {
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.myOrientation = new East();
            return initialLocationInfo;
        }
    }
}
