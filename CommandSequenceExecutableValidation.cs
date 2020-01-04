using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    struct CommandSequenceExecutableValidation
    {
        private LocationInfo whereLocationBecomesInvalid;

        public bool Valid { get; set; }
        public int InvalidCommandIndex { get; set; }

        public LocationInfo WhereLocationBecomesInvalid { get => whereLocationBecomesInvalid; set => whereLocationBecomesInvalid = value; }
    }
}
