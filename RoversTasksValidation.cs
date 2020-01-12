using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    struct RoversTasksValidation
    {
        private LocationInfo _whereLocationBecomesInvalid;

        public bool CommandsExecutionSuccess { get; set; }
        public int InvalidCommandIndex { get; set; }
        public string NameOfRover { get; set; } //not using yet
        public LocationInfo WhereCommandBecomesInvalid { get => _whereLocationBecomesInvalid; set => _whereLocationBecomesInvalid = value; }
    }
}
