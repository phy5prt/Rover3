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
                

                testRouteLocation = commandSequence[i].ExecuteCommand(testRouteLocation);
                if (testRouteLocation.withinXBounds == false || testRouteLocation.withinYBounds == false)
                {
                    //build command out come with text data 
                    //break by returning
                    testFailText = "Failed at location, command character caused out of bounds, the command sequence was aborted";
                    return testFailText;
                }
            
            }
            //for loop doing real execution of commands 
            commandExecutedText = "New location";

            return commandExecutedText;
        }
    }
}
