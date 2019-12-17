using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.Commands
{
    class MoveForward : Command
    {
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.xCoord += initialLocationInfo.myOrientation.xModifier;
            initialLocationInfo.yCoord += initialLocationInfo.myOrientation.yModifier;
            return initialLocationInfo;
        }
    }
}
