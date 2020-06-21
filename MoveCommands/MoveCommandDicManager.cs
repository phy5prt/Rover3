using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Rover3.MoveCommands.DriveCommandsNS;
using Rover3.MoveCommands.FaceCommandsNS;
using Rover3.MoveCommands.TurnCommandsNS;


namespace Rover3.MoveCommands
{
    static class MoveCommandDicManager
    {
        //this class maybe should be available on all dics so maybe they should inherit from same abstract
        //actually will need to to build the list!
       // public static IList<Orientation> commandDicsList = new List<Orientation>();
        public static IList<MoveCommand> MoveCommandStrToCmdList(string userInput)
        {
            IList<MoveCommand> moveCommandList = new List<MoveCommand>();
            MoveCommand command;
            for (int i = 0; i < userInput.Length; i++)
            {
                string userInputKey = userInput[i].ToString();
                //command = StaticMoveCommandFactoryDic.commandKeys[userInputKey];
                command = SelectCommandFromMoveCommandDics(userInputKey);
                moveCommandList.Add(command);
            }
            return moveCommandList;
        }

        public static MoveCommand SelectCommandFromMoveCommandDics(string userInputKey)
        {
            //Code smell I can remove by making dictionary manager that makes the dictionaries based on the namespaces
            //foreach namespace make dictionary
            //dictionary needs to have a getter for its name
            //so inherit from abstract class with name and getter dictionary?

            if (DriveCommandsDicCS.DriveCommandsDic.ContainsKey(userInputKey)) { return DriveCommandsDicCS.DriveCommandsDic[userInputKey]; }
            if (FaceCommandsDicCS.FaceCommandsDic.ContainsKey(userInputKey)) { return FaceCommandsDicCS.FaceCommandsDic[userInputKey]; }
            if (TurnCommandsDicCS.TurnCommandsDic.ContainsKey(userInputKey)) { return TurnCommandsDicCS.TurnCommandsDic[userInputKey]; }

            //Shouldnt ever get here
            MoveCommand command = new FaceEast();
            return command;


        }

        public static bool KeyExistsInAMoveCommandDictionary(string userInputKey)
        {
            return (DriveCommandsDicCS.DriveCommandsDic.ContainsKey(userInputKey) || FaceCommandsDicCS.FaceCommandsDic.ContainsKey(userInputKey) || TurnCommandsDicCS.TurnCommandsDic.ContainsKey(userInputKey));
        }

        //static MoveCommandDicManager()
        //{
        //    var commandDics = Assembly.GetAssembly(typeof(Orientation)).GetTypes()
        //            .Where(orientation => orientation.IsClass && !orientation.IsAbstract && orientation.IsSubclassOf(typeof(Orientation)) && (orientation.Namespace == "OrientationsNS"));




        //    //can i remove var
        //    //shouldnt name the var the same name!
        //    foreach (var orientation in orientations)
        //    {
        //        Orientation orientationInst = Activator.CreateInstance(orientation) as Orientation;
        //        orderedOrdinatesByDegreesInList.Add(orientationInst);
        //    }
        //    orderedOrdinatesByDegreesInList.OrderBy(orientationObj => orientationObj.compassDegrees);



        //}

    }
}
