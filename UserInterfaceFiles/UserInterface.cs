using Rover3.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class UserInterface
    {
        ConsoleHandler consoleHandler = new ConsoleHandler();


        //Later should request start location of rover 
        //private Orientation StartSouth = new South();
        //private int   initXCoord=0, initYCoord=0, xLowBound = 0, xHighBound = 10, yLowBound = 0, yHighBound = 10;

        //LocationInfo roverInitLocation = new LocationInfo(StartSouth, initXCoord, initYCoord, xLowBound, xHighBound, yLowBound, yHighBound );
      

       

        Rover rover = new Rover(new LocationInfo(new South(), 0, 0, 0, 10, 0, 10));//later make this a key press to add a rover to the a list of rovers accessible with a number
        public UserInterface() {
            consoleHandler.displayText(InitialInstructions());
        }
        public string InitialInstructions() {
            string initialInstructions = "";
            return initialInstructions;
        }
        public void Instructions() { }
        public void ReportLocation() { }
        

    }
}
