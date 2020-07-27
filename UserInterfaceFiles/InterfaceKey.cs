using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rover3.MoveCommands.DriveCommandsNS;
using Rover3.MoveCommands.FaceCommandsNS;
using Rover3.MoveCommands.TurnCommandsNS;
using Rover3;
using Rover3.UserInterfaceFiles;

namespace Rover3.UserInterfaceFiles
{
    public abstract class InterfaceKey : ConsoleHandler, IKeyboardKey

    {
        //uses current location not test so if called during route validation will fail
        //need to rename test location so it is assigned location or something so it is the one to check
        public void ReportLocationAllRovers()
        {
            int numberOfRovers = RoverManagerStatic.RoverDictionary.Count;
            if (numberOfRovers == 0) { DisplayText(instructionCreateRover); }
            else
            {
                StringBuilder allRoversReport = new StringBuilder(150 * numberOfRovers);
                foreach (Rover rover in RoverManagerStatic.RoverDictionary.Values)
                {
                    allRoversReport.Append(ReportLocationSingleRover(rover.CurrentLocation));

                }
                DisplayText(allRoversReport.ToString());
            }
        }

        public String instructionCreateRover = "There are currently no rovers press C to create one ";
        //currently repeating this function - if location information 

        public abstract string Key { get; }

        public abstract string KeyFunctionDescription { get; }

        public abstract void KeyAction();

    }
}
