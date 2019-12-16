using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class East: Orientation
    {
    public override string orientationName { get => "East"; }
    public override int yModifier { get => 0; }

    public override int xModifier { get => 1; }

}
}
