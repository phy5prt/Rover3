using Rover3.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class Rover //Receiver Class
    {
        public LocationInfo currentLocation; //not private because report location uses it, use get set do only getable and prvately set

        public Rover(LocationInfo initLocation) { this.currentLocation = initLocation; }
        private string testFailText = "";
        private string commandExecutedText = "";
        public string runCommandSequence(IList<Command> commandSequence)
        {
            //this may not work because may be reference so may change it !!!! Correct
            LocationInfo testRouteLocation = currentLocation; // need to clone it

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
