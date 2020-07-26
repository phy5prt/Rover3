using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    public class ConsoleHandler
    {
        public string noRoverSelectedMsg = " no rover is currently selected either create a rover or select one before giving commands";
        //this will allow unit testing
        public string GetUserInput() {

            string userInput = Console.ReadLine().ToUpper();
            return userInput;
            
        }

        public void DisplayText(string text)
        {

            Console.WriteLine(text);

        }

        
        //instead of overload can use an optional parameter and do a check to see if it was used
            public string ReportLocationSingleRover()
        {
            return (    (RoverManagerStatic.ARoverIsCurrentlySelected)? ReportLocationSingleRover(RoverManagerStatic.SelectedRover.CurrentLocation) : noRoverSelectedMsg)  ;
        }



        public string ReportLocationSingleRover(LocationInfo locationInfo)
        { //this should be roverTaskValidation it is doing too much, it should be the rover that has its location history 
          //this is always called using a rover.location so could be in the rover and reporting its own location
          //however try to keep all string building in user interface
          //would also mean do not have to assign a location what it is location for as just ask the rover its name not location what is assigned to it
            StringBuilder LocationReport = new StringBuilder(150);
            //Append Line so not on same line as what being joined to
            LocationReport.AppendLine();
            LocationReport.AppendFormat("ROVER {0}  REPORT: {1}Selected Rover Name {0}", locationInfo.locationFor, Environment.NewLine);
            LocationReport.AppendFormat("{0}Rover location is X: {1} Y:{2}", Environment.NewLine, locationInfo.XCoord.ToString(), locationInfo.YCoord.ToString());
            LocationReport.AppendFormat("{0}Rover is facing {1}", Environment.NewLine, locationInfo.myOrientation.orientationName);
            LocationReport.AppendLine();
            //Append Line so not on same line as what being joined to and if they do the same there should be a gap space

            return LocationReport.ToString();
        }


    }
}
