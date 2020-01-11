﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands
{
    class FaceNorth : MoveCommand
    {
        public override string Key { get { return "N"; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
           initialLocationInfo.myOrientation = new North();
           return initialLocationInfo;
        }
    }
}