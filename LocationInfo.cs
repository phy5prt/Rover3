using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class LocationInfo:ICloneable
    {
        


        //this need more in constructor and private class behind the get set
        public Orientation myOrientation { get; set; }
        private int _yCoord;
        public int YCoord { 
   
            get { return _yCoord; }
            set
            {
                this._yCoord = value;
                this._withinYBounds = ((_yCoord <= yHighBound) && (_yCoord >= yLowBound));

            }
        }
        private int _xCoord;
        public int XCoord
        {
            get { return _xCoord; }
            set
            {
                this._xCoord = value;
                this._withinXBounds = ((_xCoord <= xHighBound) && (_xCoord >= xLowBound));


                    }
        }

        public int xLowBound { get; }
        private bool _withinXBounds;
        public bool withinXBounds { get { return this._withinXBounds; } }
        public int yLowBound { get; }
      
        public int xHighBound { get; }
        private bool _withinYBounds;
        public bool withinYBounds { get { return this._withinYBounds; }  }
        public int yHighBound { get;  }
        



        public LocationInfo(Orientation initOrientation, int initXCoord, int initYCoord, int initXLowBound, int initXHighBound, int initYLowBound, int initYHighBound) 
        {
            this.myOrientation = initOrientation;
            this.xLowBound = initXLowBound;
            this.xHighBound = initXHighBound;
            this.yLowBound = initYLowBound;
            this.yHighBound = initYHighBound;
            this.YCoord = initYCoord;
            this.XCoord = initXCoord;

         
        }

        public object Clone()
        {
          return  new LocationInfo(this.myOrientation, this.XCoord, this.YCoord, this.xLowBound, this.xHighBound, this.yLowBound, this.yHighBound);
     
        }
    }
}
