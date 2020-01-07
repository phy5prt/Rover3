using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rover3.Commands
{
    static class StaticCommandFactoryDic
    {
        //using reflection so can satisfy open close, reflection is slow but it is only done once and this 
        //reflection isnt that slow
        //https://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class
        //https://www.youtube.com/watch?v=FGVkio4bnPQ

        public static Dictionary<string, Command> commandKeys;


        static StaticCommandFactoryDic() {
            var commands = Assembly.GetAssembly(typeof(Command)).GetTypes()
                .Where(myCommand => myCommand.IsClass && !myCommand.IsAbstract && myCommand.IsSubclassOf(typeof(Command)));

            commandKeys = new Dictionary<string, Command>();

            //can i remove var
            foreach (var command in commands) 
            {
                Command myCommand = Activator.CreateInstance(command) as Command;
                commandKeys.Add(myCommand.Key, myCommand);
            }
        }

    }
}
