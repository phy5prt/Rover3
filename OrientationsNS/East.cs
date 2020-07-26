using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class East: Orientation
    {
    public override string orientationName { get => "East"; }

    public override int compassDegrees { get => 90; }
    public override int yModifier { get => 0; }

    public override int xModifier { get => 1; }

    public override int CompassDegrees
    {
        get => 90;
    }

        public override object Clone()
    {
     return new East() as Orientation;
    }
    }
}
