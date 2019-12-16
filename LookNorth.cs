using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class LookNorth : Command
    {
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {
           initialLocationInfo.myOrientation = new North();
           return initialLocationInfo;
        }
    }
}
