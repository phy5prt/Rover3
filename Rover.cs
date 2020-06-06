using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    //comment
    class Rover : IhasKeyboardKey //Receiver Class
    {
        private LocationInfo _currentLocation;
        public LocationInfo CurrentLocation
        {
            get 
            {
                return _currentLocation;
            }
            set
            {
                this._currentLocation = value;
                TestRouteLocation = this._currentLocation.Clone() as LocationInfo;
            }
        }//not private because report location uses it, use get set do only getable and prvately set
        private LocationInfo _testRouteLocation;
        public LocationInfo TestRouteLocation 
        {
            get
            {
                return this._testRouteLocation;
            }
            set
            {
                this._testRouteLocation = value;
            }    
        } //public? PRIVATE
        public String RoverKeyName { get; }

        public string Key { get { return RoverKeyName; } }

        public string KeyFunctionDescription { get { return "Press " + RoverKeyName + " to use rover " + RoverKeyName; } }

        public Rover(String roverKeyName,LocationInfo initLocation) { this.RoverKeyName = roverKeyName; this.CurrentLocation = initLocation; }
     
        public RoverTasksValidation validateRouteOfCommandSequence(IList<MoveCommand> commandSequence, int myStartingIndexOfAllRoversCommandString)
        {
            RoverTasksValidation commandSequenceExecutableValidation = new RoverTasksValidation(RoverKeyName);

        //this resets it but we may go to this rover and come back
            //two solutions execute a rover at a time
            //retain the test info

            if (commandSequence.Count == 0) // no commands just report where you are
            {
                commandSequenceExecutableValidation.CommandsExecutionSuccess = true;
                commandSequenceExecutableValidation.RoverMoved = false;
                return commandSequenceExecutableValidation;
            }

            else {

                for (int i = 0; i < commandSequence.Count; i++)
                {

                    //if had hffhff would it know to use the last position set in same command
                    TestRouteLocation = commandSequence[i].ExecuteCommand(TestRouteLocation);
                    if (TestRouteLocation.withinXBounds == false || TestRouteLocation.withinYBounds == false)
                    {


                        commandSequenceExecutableValidation.InvalidCommandIndex = i+ myStartingIndexOfAllRoversCommandString;
                        commandSequenceExecutableValidation.WhereCommandBecomesInvalid = TestRouteLocation;
                        commandSequenceExecutableValidation.CommandsExecutionSuccess = false; //this built into setter anyway!
                        RevertTestRoverToCurrentLocation();
                        return commandSequenceExecutableValidation;
                    }
                    
                }
                commandSequenceExecutableValidation.CommandsExecutionSuccess  = true;
                commandSequenceExecutableValidation.TaskEndLocation = TestRouteLocation;
                commandSequenceExecutableValidation.RoverMoved = true;
                return commandSequenceExecutableValidation;
            }

        }
        //in future make it return telemetry
        public RoverTasksValidation ExecuteCommandSequence(IList<MoveCommand> commandSequence)
        {
         

            RoverTasksValidation commandSequenceExecutableValidation = new RoverTasksValidation(RoverKeyName);


            if (commandSequence.Count == 0) // no commands just report where you are
            {
                commandSequenceExecutableValidation.CommandsExecutionSuccess = true;
                commandSequenceExecutableValidation.RoverMoved = false;
                return commandSequenceExecutableValidation;
            }


            

            for (int i = 0; i < commandSequence.Count; i++)
            {
                CurrentLocation = commandSequence[i].ExecuteCommand(CurrentLocation);
            }
            commandSequenceExecutableValidation.CommandsExecutionSuccess = true;
                commandSequenceExecutableValidation.RoverMoved = true;

            return commandSequenceExecutableValidation;
        }

        public void RevertTestRoverToCurrentLocation() //should this be called when a location is false
        { 
            TestRouteLocation = this._currentLocation.Clone() as LocationInfo; 
        }
    }
}
