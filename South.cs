using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class South: Orientation
    {
        public override string orientationName { get => "South"; }
        public override int yModifier { get => 1; }

        public override int xModifier { get => 0; }

    }
}

