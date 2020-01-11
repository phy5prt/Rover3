using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands
{
    class MoveForward : MoveCommand
    {
        public override string Key { get { return "F"; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.XCoord += initialLocationInfo.myOrientation.xModifier;
            initialLocationInfo.YCoord += initialLocationInfo.myOrientation.yModifier;
            return initialLocationInfo;
        }
    }
}
