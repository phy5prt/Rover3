using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.Commands

{
    class MoveBackward : Command
    {
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.XCoord -= initialLocationInfo.myOrientation.xModifier;
            initialLocationInfo.YCoord -= initialLocationInfo.myOrientation.yModifier;
            return initialLocationInfo;
        }
    }
}
