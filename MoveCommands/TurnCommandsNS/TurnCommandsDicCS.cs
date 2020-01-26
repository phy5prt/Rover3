using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rover3.MoveCommands.TurnCommandsNS
{
 

        static class TurnCommandsDicCS
        {
            public static Dictionary<string, MoveCommand> TurnCommandsDic = new Dictionary<string, MoveCommand>();

            //should driveCommands be capitalised
            static TurnCommandsDicCS()
            {

                //is this reflection better https://youtu.be/nqAHJmpWLBg?t=972 
                var turnCommands = Assembly.GetAssembly(typeof(MoveCommand)).GetTypes()
                    .Where(turnCommand => turnCommand.IsClass && !turnCommand.IsAbstract && turnCommand.IsSubclassOf(typeof(MoveCommand)) && (turnCommand.Namespace == "Rover3.MoveCommands.TurnCommandsNS"));

                 


                //can i remove var
                //shouldnt name the var the same name!
                foreach (var turnCommand in turnCommands)
                {
                    MoveCommand turnCommandInst = Activator.CreateInstance(turnCommand) as MoveCommand;
                    TurnCommandsDic.Add(turnCommandInst.Key, turnCommandInst);
                }

            }

        }
    
}
