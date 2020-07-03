using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    //comment
    class Rover : IKeyboardKey //Receiver Class
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

        public Rover(String roverKeyName,LocationInfo initLocation) { this.RoverKeyName = roverKeyName; this.CurrentLocation = (LocationInfo)initLocation.Clone(); }
     
        public RoverTasksValidation validateRouteOfCommandSequence(IList<MoveCommand> commandSequence)
        {
            RoverTasksValidation commandSequenceExecutableValidation = new RoverTasksValidation(RoverKeyName);
            //should create a test route
            //shouldnt clear it
            //should only be reset on all rovers failing (but we only need it to fail once and we will stop checking the rest)
            //or when location updates
  

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


                        commandSequenceExecutableValidation.InvalidCommandIndex = i;
                        commandSequenceExecutableValidation.WhereCommandBecomesInvalid = (LocationInfo)TestRouteLocation.Clone();
                        commandSequenceExecutableValidation.CommandsExecutionSuccess = false; //this built into setter anyway!
                        //we reverted here but shouldnt
                        return commandSequenceExecutableValidation;
                    }
                    
                }
                commandSequenceExecutableValidation.CommandsExecutionSuccess  = true;
                commandSequenceExecutableValidation.TaskEndLocation = (LocationInfo)TestRouteLocation.Clone();
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
