using Rover3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    //this class shoul just be called report

//A report should be:
        //Rover A (ABC)
        //Route check was successful (All rovers)
                        
        
        // per rover 
        //rover move possible
       //check unsuccessful at 
                            

        //If successful - 
            //rover exectuted string
            //rover position
            //rover facing

//A group rover report should be   //no group rover 
        //The rovers   -   could not / were moved
        // rover reports


    struct RoversTasksValidation //should this be an interface
    {
        private LocationInfo _whereLocationBecomesInvalid;

        private bool _commandExecutionSuccess;
        public bool CommandsExecutionSuccess
        {
            get { return this._commandExecutionSuccess; }
            set {
                //should this just be dont manually so dont confuse things
                this._commandExecutionSuccess = value;
                if (value == false) { RoverMoved = false; }
                //not the otherway around though because may not have moved if just given an empty  command
            } 
        }
        public bool RoverMoved { get; set; }
        public int InvalidCommandIndex { get; set; }
        public string NameOfRover { get; set; } //not using yet
        public LocationInfo WhereCommandBecomesInvalid { get => _whereLocationBecomesInvalid; set => _whereLocationBecomesInvalid = value; }

        private string _validationReport;
        public string ValidationReport 
        {
            get
            {
                //if i just use get and get the values to build the report

                // pinched from userInterface
                //put in the values so not requiring user to call method only after changing valiables or do it in a constructor
                

                    //structure 
                    //success fail
                    // move not move
                    //indivdual rover reports
                    //last rover or failed rover report

                    //make single rover report then in the opperator overload add to beggining or end the summary

                //stringBuilder !!!!!!!!!!!
                 
                    //string reachedWouldHaveReached;
                    // reachedWouldHaveReached = (RoverMoved) ? " moved to ":" could Reach " }
                    //_validationReport = string.Format("Rover(s) command report: The rover(s) Selected rover name: ")  = ;
                    //LocationReport += RoverManagerStatic.SelectedRover.RoverKeyName + " ";
                    //LocationReport += "rover location: ";
                    //LocationReport += "x location is ";
                    //LocationReport += RoverManagerStatic.SelectedRover.CurrentLocation.XCoord.ToString() + ". ";
                    //LocationReport += "y location is ";
                    //LocationReport += RoverManagerStatic.SelectedRover.CurrentLocation.YCoord.ToString() + ". ";
                    //LocationReport += "rover is facing ";
                    //LocationReport += RoverManagerStatic.SelectedRover.CurrentLocation.myOrientation.orientationName + ". ";
                    return _validationReport;
             }

            } 
         //functionality for + oprerator will also allow +=
    
            public static RoversTasksValidation operator +(RoversTasksValidation firstAddedRoversTasksValidation, RoversTasksValidation secondAddedRoversTasksValidation) 
            {

            //get report based off rover a information
            //make into string
            //get report based off rover a information
            //add to own string
            //set bools to match
            //now when someone asks this object for report it will give its own report AB success A succss B success
            //but then if added again with have this string build maybe shouldnt add reports

            //when you query it 
            //rover mov



            //both false returns false, fi either false returns false if both true returns true
            RoversTasksValidation roversTasksValidationResult = new RoversTasksValidation();
                roversTasksValidationResult.CommandsExecutionSuccess = (firstAddedRoversTasksValidation.CommandsExecutionSuccess && secondAddedRoversTasksValidation.CommandsExecutionSuccess);
                roversTasksValidationResult.RoverMoved = (firstAddedRoversTasksValidation.RoverMoved && secondAddedRoversTasksValidation.RoverMoved);

            if (roversTasksValidationResult.CommandsExecutionSuccess) {
                RoversTasksValidationResult.ValidationRep
                    //if true

            //the rover were success ...
            //false rover were fail ...
            +roverReport
                }
                //Build report string per rover then make string names

            //we will use this to say Rovers : A B C routes were possible however Rovwer D went out of bounds at (or would crash with B at)
                roversTasksValidationResult.NameOfRover = (firstAddedRoversTasksValidation.NameOfRover + secondAddedRoversTasksValidation.NameOfRover);
            roversTasksValidationResult += report


            return roversTasksValidationResult;

            }
    }

}
