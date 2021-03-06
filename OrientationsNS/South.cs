﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class South: Orientation
    {
        public override string orientationName { get => "South"; }
        public override int compassDegrees { get => 180; }
        public override int yModifier { get => -1; }

        public override int xModifier { get => 0; }
        public override int CompassDegrees
        {
            get => 180;
        }
        public override object Clone()
        {
            return new South() as Orientation;
        }

    }
}

