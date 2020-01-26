using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands.FaceCommandsNS
{
    class FaceEast : MoveCommand 
    {
        public override string Key {get{ return "E"; } }
        public override string KeyFunctionDescription { get {return " Press E to turn rover on the spot to face East "; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.myOrientation = new East();
            return initialLocationInfo;
        }
    }
}
