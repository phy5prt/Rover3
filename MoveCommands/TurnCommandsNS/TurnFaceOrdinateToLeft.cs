using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands.TurnCommandsNS
{
    class TurnFaceOrdinateToLeft : MoveCommand, ITurnRelativeToFacingDirection
    {
        
            public override string Key { get { return "L"; } }
            public override string KeyFunctionDescription { get { return " Press L to turn rover on the spot to face in the direction of the ordinate to it's left "; } }
            public override LocationInfo ExecuteCommand(LocationInfo initialLocationInfo)
            {

            initialLocationInfo.myOrientation = OrientationToTurnTo(initialLocationInfo.myOrientation, -1);
                return initialLocationInfo;
            }

           public Orientation OrientationToTurnTo(Orientation currentlyFacing, int noOrientationRightToTurn)
           {
            //orientationToTurnTo = //here enum;
            return currentlyFacing;//later base on enum

           }
    }
    
}
