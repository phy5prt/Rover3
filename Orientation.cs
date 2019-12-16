using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    abstract class Orientation
    {
        public abstract string orientationName { get; }
        public abstract int yModifier { get; }

        public abstract int xModifier { get; }
    }
}
