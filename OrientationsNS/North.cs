using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class North:Orientation
    {
        public override string orientationName { get => "North"; }

        public override int compassDegrees { get => 0; }
        public override int yModifier { get => 1; }

        public override int xModifier { get => 0; }
        public override int CompassDegrees
        {
            get => 0;
        }
        public override object Clone()
        {
            return new North() as Orientation;
        }

    }
}
