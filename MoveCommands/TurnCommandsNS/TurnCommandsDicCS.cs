using Rover3.MoveCommands.FaceCommandsNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rover3.MoveCommands.TurnCommandsNS
{


    static class TurnCommandsDicCS
    {
        public static Dictionary<string, MoveCommand> TurnCommandsDic = new Dictionary<string, MoveCommand>();
        private static List<Orientation> OrderOfDirections = new List<Orientation>() { new North(), new East(), new South(), new West() };//not using reflection so dont need to be using it anywhere
        public static Orientation GetNewDirectionRelativeToDirection(Orientation startDirection, int turns)
        {

            //int startDirectionIndex = OrderOfDirections.IndexOf(startDirection);
            int startDirectionIndex = OrderOfDirections.FindIndex(r => r.GetType() == startDirection.GetType());
            int newDirectionIndex;
           
            int rawIndex = startDirectionIndex + turns;

            if (rawIndex < 0)             {
                Double adjustedDBL = Math.Floor((double)(rawIndex * -1) / OrderOfDirections.Count);
                int adjustedInt = Convert.ToInt32(adjustedDBL);
                newDirectionIndex = rawIndex + (adjustedInt + 1) * OrderOfDirections.Count;
            }
            else
            {
                newDirectionIndex = rawIndex % OrderOfDirections.Count;
            }


            return OrderOfDirections[newDirectionIndex];
        }

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
