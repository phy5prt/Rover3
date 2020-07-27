using System;
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
    public class Q : InterfaceKey

    {
        public override string Key { get { return "Q"; } }

        public override string KeyFunctionDescription { get { return "Press Q to quit."; } }

        public override void KeyAction()
        {
            Environment.Exit(0);

        }
    }
}
