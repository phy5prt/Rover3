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
    public class K : InterfaceKey

    {
        //why not a list ... because can use contains for value
       
        public override string Key { get { return "K"; } }

        public override string KeyFunctionDescription { get { return "Press K to get a list of keys and their descriptions."; } }

        public override void KeyAction()
        {
            StringBuilder sb = new StringBuilder(200);
            //how can this be a key for reporting on itself
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Interface keys must be entered as a single character");
            foreach (IKeyboardKey dicEntry in UserInterfaceDic.interfaceDic.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Movement, Orientation, Turn, and Select Rover commands can be entered as a long string");
            sb.AppendLine();
            sb.AppendLine("Available Rovers");
            sb.AppendLine();

            foreach (IKeyboardKey dicEntry in RoverManagerStatic.RoverDictionary.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Drive Commands");
            sb.AppendLine();

            //if (DriveCommandsDicCS.DriveCommandsDic.ContainsKey(userInputKey)) { return DriveCommandsDicCS.DriveCommandsDic[userInputKey]; }
            //if (FaceCommandsDicCS.FaceCommandsDic.ContainsKey(userInputKey)) { return FaceCommandsDicCS.FaceCommandsDic[userInputKey]; }
            //if (TurnCommandsDicCS.TurnCommandsDic.ContainsKey(userInputKey)
            foreach (IKeyboardKey dicEntry in DriveCommandsDicCS.DriveCommandsDic.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine("Face Commands");
            sb.AppendLine();

            foreach (IKeyboardKey dicEntry in FaceCommandsDicCS.FaceCommandsDic.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine("Turns Commands");
            sb.AppendLine();

            foreach (IKeyboardKey dicEntry in TurnCommandsDicCS.TurnCommandsDic.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }




            DisplayText(sb.ToString());

        }
    }
}
