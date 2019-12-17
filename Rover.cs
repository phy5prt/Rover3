using Rover3.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class Rover //Receiver Class
    {
        private LocationInfo currentLocation; 

        public Rover(LocationInfo initLocation) { this.currentLocation = initLocation; }
        private string testFailText = "";
        private string commandExecutedText = "";
        public string runCommandSequence(IList<Command> commandSequence)
        {
            //this may not work because may be reference so may change it
            LocationInfo testRouteLocation = currentLocation;

            for (int i = 0; i < commandSequence.Count; i++) {
                //pseudo  Command nextCommand = dictionary(commandSequence[i])

                testRouteLocation = nextCommand.execute(testRouteLocation);
                if (testRouteLocation.withinBoundsX == false || testRouteLocation.withinBoundsY)

                    //build command out come with text data 
                    //break by returning
                     testFailText = "Failed at location, command character caused out of bounds, the command sequence was aborted";
                return testFailText;
            
            }
            //for loop doing real execution of commands 
            commandExecutedText = "New location";

            return commandExecutedText;
        }
    }
}
