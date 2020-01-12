using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    class Rover //Receiver Class
    {
        public LocationInfo currentLocation; //not private because report location uses it, use get set do only getable and prvately set

        public String RoverKeyName { get; }
        public Rover(String roverKeyName,LocationInfo initLocation) { this.RoverKeyName = roverKeyName; this.currentLocation = initLocation; }
     
        public RoversTasksValidation validateRouteOfCommandSequence(IList<MoveCommand> commandSequence)
        {
            RoversTasksValidation commandSequenceExecutableValidation = new RoversTasksValidation();

            LocationInfo testRouteLocation = currentLocation.Clone() as LocationInfo;


            for (int i = 0; i < commandSequence.Count; i++)
            {


                testRouteLocation = commandSequence[i].ExecuteCommand(testRouteLocation);
                if (testRouteLocation.withinXBounds == false || testRouteLocation.withinYBounds == false)
                {


                    commandSequenceExecutableValidation.InvalidCommandIndex = i;
                    commandSequenceExecutableValidation.WhereCommandBecomesInvalid = testRouteLocation;
                    commandSequenceExecutableValidation.CommandsExecutionSuccess = false;
                    return commandSequenceExecutableValidation;
                }

            }

            return ExecuteCommandSequence(commandSequence); //if I have multiple rovers will not execute till all tested
        }

        public RoversTasksValidation ExecuteCommandSequence(IList<MoveCommand> commandSequence)
        {
            //If rover was real would be telemetry and checks as it went so all though not doing any
            //validating I'm not going to make it void

            RoversTasksValidation commandSequenceExecutableValidation = new RoversTasksValidation();

            for (int i = 0; i < commandSequence.Count; i++)
            {
                currentLocation = commandSequence[i].ExecuteCommand(currentLocation);
            }
            commandSequenceExecutableValidation.CommandsExecutionSuccess = true;

            return commandSequenceExecutableValidation;
        }
    }
}
