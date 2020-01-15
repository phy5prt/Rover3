using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rover3.MoveCommands
{
    static class StaticMoveCommandFactoryDic
    {
        //is it a factory its not making them is is it 
        //using reflection so can satisfy open close, reflection is slow but it is only done once and this 
        //reflection isnt that slow
        //https://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class
        //https://www.youtube.com/watch?v=FGVkio4bnPQ

        public static Dictionary<string, MoveCommand> commandKeys;


        static StaticMoveCommandFactoryDic() {
            var moveCommands = Assembly.GetAssembly(typeof(MoveCommand)).GetTypes()
                .Where(myCommand => myCommand.IsClass && !myCommand.IsAbstract && myCommand.IsSubclassOf(typeof(MoveCommand)));

            commandKeys = new Dictionary<string, MoveCommand>();

            //can i remove var
            foreach (var moveCommand in moveCommands) 
            {
                MoveCommand myCommand = Activator.CreateInstance(moveCommand) as MoveCommand;
                commandKeys.Add(myCommand.Key, myCommand);
            }
        }


        //here could use a switch later dictionary of dictionaries so can switch between rovers
        public static IList<MoveCommand> MoveCommandStrToCmdList(string userInput)
        {
            IList<MoveCommand> moveCommandList = new List<MoveCommand>();
            MoveCommand command;
            for (int i = 0; i < userInput.Length; i++)
            {
                string userInputKey = userInput[i].ToString();
                command = StaticMoveCommandFactoryDic.commandKeys[userInputKey];
                moveCommandList.Add(command);
            }
            return moveCommandList;
        }
    }
}
