using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class Rover
    {
        private LocationInfo currentLocation; 

        public Rover(LocationInfo initLocation) { this.currentLocation = initLocation; }

        public commandOutCome runCommandSequence(string commandSequence)
        {
            //this may not work because may be reference so may change it
            LocationInfo testRouteLocation = currentLocation;

            for (int i = 0; i < commandSequence.Length; i++) {
                //pseudo  Command nextCommand = dictionary(commandSequence[i])

                testRouteLocation = nextCommand.execute(testRouteLocation);   
            if(testRouteLocation.withinBoundsX == false || testRouteLocation.withinBoundsY)

               //build command out come with text data 
               //break by returning


            
            }
            //for loop doing real execution of commands 
        }
    }
}
