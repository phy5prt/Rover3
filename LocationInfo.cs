using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class LocationInfo
    {
        


        //this need more in constructor and private class behind the get set
        public Orientation myOrientation { get; set; }
        public int yCoord { get { return yCoord; } 
            set {
                yCoord = value;
                withinYBounds = ((yCoord <= yHighBound) && (yCoord >= yLowBound));
            
                    } 
        }
        public int xCoord
        {
            get { return xCoord; }
            set
            {
                xCoord = value;
                withinYBounds = ((xCoord <= xHighBound) && (xCoord >= xLowBound));


                    }
        }

        public int xLowBound { get; }
        public bool withinXBounds { get; }
        public int yLowBound { get; }
      
        public int xHighBound { get; }
        public bool withinYBounds { get; set; }
        public int yHighBound { get;  }
        



        public LocationInfo(Orientation initOrientation, int initYCoord, int initXCoord, int initXLowBound, int initXHighBound, int initYLowBound, int initYHighBound) 
        {
            this.myOrientation = initOrientation;
            this.xLowBound = initXLowBound;
            this.xHighBound = initXHighBound;
            this.yLowBound = initYLowBound;
            this.yHighBound = initYHighBound;
            this.yCoord = initYCoord;
            this.xCoord = initXCoord;

         
        }
    }
}
