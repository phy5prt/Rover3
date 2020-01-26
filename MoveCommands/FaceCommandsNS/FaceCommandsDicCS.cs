using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rover3.MoveCommands.FaceCommandsNS
{
    //    //is it a factory its not making them is is it 
    //    //using reflection so can satisfy open close, reflection is slow but it is only done once and this 
    //    //reflection isnt that slow
    //    //https://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class
    //    //https://www.youtube.com/watch?v=FGVkio4bnPQ


    //        //is this reflection better https://youtu.be/nqAHJmpWLBg?t=972 
    //        var moveCommands = Assembly.GetAssembly(typeof(MoveCommand)).GetTypes()
    //            .Where(myCommand => myCommand.IsClass && !myCommand.IsAbstract && myCommand.IsSubclassOf(typeof(MoveCommand)));


    static class FaceCommandsDicCS
    {
        public static Dictionary<string, MoveCommand> FaceCommandsDic = new Dictionary<string, MoveCommand>();

        //should driveCommands be capitalised
        static FaceCommandsDicCS()
        {

            //is this reflection better https://youtu.be/nqAHJmpWLBg?t=972 
            var faceCommands = Assembly.GetAssembly(typeof(MoveCommand)).GetTypes()
                .Where(faceCommand => faceCommand.IsClass && !faceCommand.IsAbstract && faceCommand.IsSubclassOf(typeof(MoveCommand)) && (faceCommand.Namespace == "FaceCommandsNS"));

       


            //can i remove var
            //shouldnt name the var the same name!
            foreach (var faceCommand in faceCommands)
            {
                MoveCommand faceCommandInst = Activator.CreateInstance(faceCommand) as MoveCommand;
                FaceCommandsDic.Add(faceCommandInst.Key, faceCommandInst);
            }

        }

    }
}