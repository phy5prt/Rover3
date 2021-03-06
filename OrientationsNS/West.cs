﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class West : Orientation
    {
        public override string orientationName { get => "West"; }
        public override int compassDegrees { get => 270; }
        public override int yModifier { get => 0; }

        public override int xModifier { get => -1; }
        public override int CompassDegrees
        {
            get => 270;
        }
        public override object Clone()
        {
            return new West() as Orientation;
        }

    }
    
}
