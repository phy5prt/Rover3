using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class Rover //Receiver Class
    {
        public LocationInfo currentLocation; //not private because report location uses it, use get set do only getable and prvately set
        private String _roverKeyName; //this needs to stay unique as it is used with the dictionary maybe singleton pattern is worth looking up if one like me exists delete me?
        public String RoverKeyName { get; }
        public Rover(String roverKeyName,LocationInfo initLocation) { this._roverKeyName = roverKeyName; this.currentLocation = initLocation; }
     
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
