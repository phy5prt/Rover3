using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;



namespace Rover3.MoveCommands.DriveCommandsNS 
{
    //    //is it a factory its not making them is is it 
    //    //using reflection so can satisfy open close, reflection is slow but it is only done once and this 
    //    //reflection isnt that slow
    //    //https://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class
    //    //https://www.youtube.com/watch?v=FGVkio4bnPQ


    //        //is this reflection better https://youtu.be/nqAHJmpWLBg?t=972 
    //        var moveCommands = Assembly.GetAssembly(typeof(MoveCommand)).GetTypes()
    //            .Where(myCommand => myCommand.IsClass && !myCommand.IsAbstract && myCommand.IsSubclassOf(typeof(MoveCommand)));
    

    static class DriveCommandsDicCS 
    {
           public static Dictionary<string, MoveCommand> DriveCommandsDic = new Dictionary<string, MoveCommand>();

        //should driveCommands be capitalised
       static DriveCommandsDicCS() {

            //is this reflection better https://youtu.be/nqAHJmpWLBg?t=972 
            var driveCommands = Assembly.GetAssembly(typeof(MoveCommand)).GetTypes()
                .Where(driveCommand => driveCommand.IsClass && !driveCommand.IsAbstract && driveCommand.IsSubclassOf(typeof(MoveCommand))&& (driveCommand.Namespace == "Rover3.MoveCommands.DriveCommandsNS"));
            
           
              

                //can i remove var
                //shouldnt name the var the same name!
            foreach (var driveCommand in driveCommands)
            {
                MoveCommand driveCommandInst = Activator.CreateInstance(driveCommand) as MoveCommand;
                DriveCommandsDic.Add(driveCommandInst.Key, driveCommandInst);
            }

       }
        
    }
}
