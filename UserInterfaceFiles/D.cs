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
    public class D : InterfaceKey

    {
        public override string Key { get { return "D"; } }

        public override string KeyFunctionDescription { get { return "Press D to destroy currently selected Rover."; } }

        public override void KeyAction()
        {
            if (RoverManagerStatic.ARoverIsCurrentlySelected)
            {
                String selectedRoverKey = RoverManagerStatic.SelectedRover.Key;

                if (RoverManagerStatic.RemoveSelectedRoverFromDictionary())
                {

                    StringBuilder sb = new StringBuilder("Rover : " + selectedRoverKey + "  has been destroyed.", 150);

                    if (RoverManagerStatic.RoverDictionary.Count < 1)
                    {
                        sb.AppendLine();
                        sb.AppendLine(instructionCreateRover);

                    }
                    DisplayText(sb.ToString());
                }
                else
                {
                    DisplayText("Rover : " + selectedRoverKey + " does not exist so could not be destroyed");

                };

            }
            else { DisplayText("No Rover currently selected, select a rover then return then press d to delete it"); }
        }
    }
}
