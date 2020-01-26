using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.MoveCommands
{
    class MoveCommandDicManager
    {
        //this class maybe should be available on all dics so maybe they should inherit from same abstract
        //actually will need to to build the list!
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
