using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class LocationInfo
    {

        //this need more in constructor and private class behind the get set
        public Orientation myOrientation { get; set; }
        public int yCoord { get => yCoord; 
            set {
            withinYBounds = ((yCoord<=yHighBound) &&(yCoord>=yLowBound))
            } 
        }
        public int xCoord { get; set; }

        public int xLowBound { get; }
        public bool withinXBounds { get; }
        public int yLowBound { get; }
      
        public int xHighBound { get; }
        public bool withinYBounds { get; set; }
        public int yHighBound { get;  }
        



        public LocationInfo(Orientation initOrientation, int initYCoord, int initXCoord) 
        {
            this.myOrientation = initOrientation;
            this.yCoord = initYCoord;
            this.xCoord = initXCoord;
        }
    }
}
