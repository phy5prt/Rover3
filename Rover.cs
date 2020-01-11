using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class Rover //Receiver Class
    {
        public LocationInfo currentLocation; //not private because report location uses it, use get set do only getable and prvately set

        public Rover(LocationInfo initLocation) { this.currentLocation = initLocation; }
     
        public CommandSequenceExecutableValidation runCommandSequence(IList<MoveCommand> commandSequence)
        {
            CommandSequenceExecutableValidation commandSequenceExecutableValidation = new CommandSequenceExecutableValidation();
              LocationInfo testRouteLocation = currentLocation.Clone() as LocationInfo;
               

            for (int i = 0; i < commandSequence.Count; i++) {
                

                testRouteLocation = commandSequence[i].ExecuteCommand(testRouteLocation);
                if (testRouteLocation.withinXBounds == false || testRouteLocation.withinYBounds == false)
                {
                  
                   
                    commandSequenceExecutableValidation.InvalidCommandIndex = i;
                    commandSequenceExecutableValidation.WhereLocationBecomesInvalid = testRouteLocation;
                    commandSequenceExecutableValidation.CommandsExecutionSuccess = false;
                    return commandSequenceExecutableValidation;
                }
            
            }

            for (int i = 0; i < commandSequence.Count; i++)
            {
                currentLocation = commandSequence[i].ExecuteCommand(currentLocation);
            }
            commandSequenceExecutableValidation.CommandsExecutionSuccess = true;

            return commandSequenceExecutableValidation;
        }
    }
}
