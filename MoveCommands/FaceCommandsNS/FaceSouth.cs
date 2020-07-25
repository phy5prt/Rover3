using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands.FaceCommandsNS
{
    class FaceSouth : MoveCommand
    {
      
    
        public override string Key { get { return "S"; } }
        public override string KeyFunctionDescription { get { return " Press S to turn rover on the spot to face South "; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.myOrientation = new South();
            return initialLocationInfo;
        }
    }
}
