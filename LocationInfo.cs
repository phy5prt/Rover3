using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class LocationInfo : ICloneable
    {



        //this need more in constructor and private class behind the get set
        public Orientation myOrientation { get; set; }
        public String locationFor {get;set;}
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

        public int xLowBound { get; set; }
        private bool _withinXBounds;
        public bool withinXBounds { get { return this._withinXBounds; } }
        public int yLowBound { get; set; }
      
        public int xHighBound { get; set; } //previously these were only get so could only be set in constructor 
        //however because having to make an initial location info then change it in order to use the command dictionary
        //to set the initial orientation it now needs to be overwritable - recallinh constructor would be nasty
        private bool _withinYBounds;
        public bool withinYBounds { get { return this._withinYBounds; }  }
        public int yHighBound { get; set; }
        



        public LocationInfo(Orientation initOrientation, int initXCoord, int initYCoord, int initXLowBound, int initXHighBound, int initYLowBound, int initYHighBound, string initLocationFor) 
        {
          
            this.xLowBound = initXLowBound;
            this.xHighBound = initXHighBound;
            this.yLowBound = initYLowBound;
            this.yHighBound = initYHighBound;
            this.myOrientation = initOrientation;
            this.XCoord = initXCoord;
            this.YCoord = initYCoord;
            this.locationFor = initLocationFor;




        }

        public object Clone()
        {
          return  new LocationInfo(this.myOrientation, this.XCoord, this.YCoord, this.xLowBound, this.xHighBound, this.yLowBound, this.yHighBound, this.locationFor);
     
        }
    }
}
