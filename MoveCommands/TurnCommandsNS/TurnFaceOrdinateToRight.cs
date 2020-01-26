using Rover3.MoveCommands.TurnCommandsNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands.TurnCommands
{
    class TurnFaceOrdinateToRight : MoveCommand, ITurnRelativeToFacingDirection
    {
  
        public override string Key { get { return "R"; } }
        public override string KeyFunctionDescription { get { return " Press R to turn rover on the spot to face in the direction of the ordinate to it's right "; } }
        public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
        {

            initialLocationInfo.myOrientation = OrientationToTurnTo(initialLocationInfo.myOrientation, 1);
            return initialLocationInfo;
        }

        public Orientation OrientationToTurnTo(Orientation currentlyFacing, int noOrientationRightToTurn)
        {
            //orientationToTurnTo = //here enum;
            return currentlyFacing;//later base on enum

        }
    }
}
