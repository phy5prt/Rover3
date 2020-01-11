using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    struct CommandSequenceExecutableValidation
    {
        private LocationInfo _whereLocationBecomesInvalid;

        public bool CommandsExecutionSuccess { get; set; }
        public int InvalidCommandIndex { get; set; }

        public LocationInfo WhereLocationBecomesInvalid { get => _whereLocationBecomesInvalid; set => _whereLocationBecomesInvalid = value; }
    }
}
