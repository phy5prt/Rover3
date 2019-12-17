using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.Commands
{
    class MoveBackwards : Command
    {
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.xCoord -= initialLocationInfo.myOrientation.xModifier;
            initialLocationInfo.yCoord -= initialLocationInfo.myOrientation.yModifier;
            return initialLocationInfo;
        }
    }
}
