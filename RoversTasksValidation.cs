using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    //this class shoul just be called report
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
                // pinched from userInterface
                public string ReportLocation()//put in the values so not requiring user to call method only after changing valiables or do it in a constructor
                {

                    //structure 
                    //success fail
                    // move not move
                    //indivdual rover reports
                    //last rover or failed rover report

                    //make single rover report then in the opperator overload add to beggining or end the summary


                    string reachedWouldHaveReached;
                     reachedWouldHaveReached = (RoverMoved) ? " moved to ":" could Reach " }
                    _validationReport = string.Format("Rover(s) command report: The rover(s) Selected rover name: ")  = ;
                    LocationReport += RoverManagerStatic.SelectedRover.RoverKeyName + " ";
                    LocationReport += "rover location: ";
                    LocationReport += "x location is ";
                    LocationReport += RoverManagerStatic.SelectedRover.CurrentLocation.XCoord.ToString() + ". ";
                    LocationReport += "y location is ";
                    LocationReport += RoverManagerStatic.SelectedRover.CurrentLocation.YCoord.ToString() + ". ";
                    LocationReport += "rover is facing ";
                    LocationReport += RoverManagerStatic.SelectedRover.CurrentLocation.myOrientation.orientationName + ". ";
                    return LocationReport;
                }






                return _validationReport;
            } 
        } 
    }
    public void operator + (RoversTasksValidation addedRoversTasksValidation) 
    {
        //both false returns false, fi either false returns false if both true returns true
        this.CommandsExecutionSuccess = (this.CommandsExecutionSuccess && addedRoversTaskValidation.ExecutionValidaionSuccess);
        this.RoverMoved = (this.RoverMoved && addedRoversTasksValidation.RoverMoved);

    }
}
