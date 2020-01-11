using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands
{
    class FaceWest : MoveCommand
    {
        public override string Key { get { return "W"; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.myOrientation = new West();
            return initialLocationInfo;
        }
    }
   
}
