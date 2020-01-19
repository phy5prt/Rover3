using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    //comment
    class Rover //Receiver Class
    {
        public LocationInfo currentLocation; //not private because report location uses it, use get set do only getable and prvately set

        public String RoverKeyName { get; }
        public Rover(String roverKeyName,LocationInfo initLocation) { this.RoverKeyName = roverKeyName; this.currentLocation = initLocation; }
     
        public RoversTasksValidation validateRouteOfCommandSequence(IList<MoveCommand> commandSequence)
        {
            RoversTasksValidation commandSequenceExecutableValidation = new RoversTasksValidation();

            LocationInfo testRouteLocation = currentLocation.Clone() as LocationInfo;

            if (commandSequence.Count == 0) // no commands just report where you are
            {
                commandSequenceExecutableValidation.CommandsExecutionSuccess = true;
                commandSequenceExecutableValidation.RoverMoved = false;
                return commandSequenceExecutableValidation;
            }

            else {

                for (int i = 0; i < commandSequence.Count; i++)
                {


                    testRouteLocation = commandSequence[i].ExecuteCommand(testRouteLocation);
                    if (testRouteLocation.withinXBounds == false || testRouteLocation.withinYBounds == false)
                    {


                        commandSequenceExecutableValidation.InvalidCommandIndex = i;
                        commandSequenceExecutableValidation.WhereCommandBecomesInvalid = testRouteLocation;
                        commandSequenceExecutableValidation.CommandsExecutionSuccess = false; //this built into setter anyway!
                        return commandSequenceExecutableValidation;
                    }

                }
                commandSequenceExecutableValidation.CommandsExecutionSuccess  = true;
                commandSequenceExecutableValidation.RoverMoved = true;
                return commandSequenceExecutableValidation;
            }

        }

        public RoversTasksValidation ExecuteCommandSequence(IList<MoveCommand> commandSequence)
        {
            //If rover was real would be telemetry and checks as it went so all though not doing any
            //validating I'm not going to make it void

            RoversTasksValidation commandSequenceExecutableValidation = new RoversTasksValidation();


            if (commandSequence.Count == 0) // no commands just report where you are
            {
                commandSequenceExecutableValidation.CommandsExecutionSuccess = true;
                commandSequenceExecutableValidation.RoverMoved = false;
                return commandSequenceExecutableValidation;
            }


            

            for (int i = 0; i < commandSequence.Count; i++)
            {
                currentLocation = commandSequence[i].ExecuteCommand(currentLocation);
            }
            commandSequenceExecutableValidation.CommandsExecutionSuccess = true;
                commandSequenceExecutableValidation.RoverMoved = true;

            return commandSequenceExecutableValidation;
        }
    }
}
