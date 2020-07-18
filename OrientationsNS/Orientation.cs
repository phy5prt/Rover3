using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    public abstract class Orientation : ICloneable
    {
        public abstract string orientationName { get;  }
        public abstract int compassDegrees { get;  }
        public abstract int yModifier { get;  }

        public abstract int xModifier { get; }

        //public Orientation(string orientationName, int compassDegrees, int yModifier, int xModifier) 
        //{
        //    this.orientationName = orientationName;
        //    this.compassDegrees = compassDegrees;
        //    this.yModifier = yModifier;
        //    this.xModifier = xModifier;
        //}

        public abstract object Clone(); //are we really cloning the values dont change 
        //shouldnt we just find the type and instantiate it 
        //if the values dont change does it matter if we just provide a reference 
        //rather than cloning
            

        
    }
}
