using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
   struct ResultOfCommandSequenceValidation
    {
       public bool valid { get; set; }
        public string errorText { get; set; }

        //public ValidateCommandSequence(bool succeeded, string failInformation = "")
        //{
        //    this.succeeded = succeeded;
        //    if (failInformation == "") { this.failInformation = this.succeeded ? "succeeded" : "failed"; }
        //    this.failInformation = failInformation;
        //}

    }
}
