﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands
{
    class FaceSouth : MoveCommand
    {
        public override string Key { get { return "S"; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
            initialLocationInfo.myOrientation = new South();
            return initialLocationInfo;
        }
    }
}