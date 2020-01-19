using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
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
    }
}
