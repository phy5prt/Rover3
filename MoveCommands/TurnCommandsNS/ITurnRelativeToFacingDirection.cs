using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands.TurnCommandsNS
{
    interface ITurnRelativeToFacingDirection
    {
        public abstract Orientation OrientationToTurnTo(Orientation currentlyFacing, int noOrientationRightToTurn);
    }
}
