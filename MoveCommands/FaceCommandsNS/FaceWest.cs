using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands.FaceCommandsNS
{
    class FaceWest : MoveCommand
    {
        public override string Key { get { return "W"; } }
        public override string KeyFunctionDescription { get { return " Press W to turn rover on the spot to face West "; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.myOrientation = new West();
            return initialLocationInfo;
        }
    }
   
}
