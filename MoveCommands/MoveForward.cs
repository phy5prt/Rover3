using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands
{
    class MoveForward : MoveCommand
    {
        public override string Key { get { return "F"; } }

        public override string KeyFunctionDescription { get { return " Press F to drive the rover forwards one grid space"; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.XCoord += initialLocationInfo.myOrientation.xModifier;
            initialLocationInfo.YCoord += initialLocationInfo.myOrientation.yModifier;
            return initialLocationInfo;
        }
    }
}
