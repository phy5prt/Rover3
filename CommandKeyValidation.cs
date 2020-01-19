using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{


    struct CommandKeyValidation   //replace with roversTasksValidation and 
    {
        private string _errorText;

        public string ErrorText
        {
            get
            {
                return this._errorText;
            }
            set
            {
                this._errorText = value;
            }
        }

        private bool _valid;
        public bool Valid
        {
            get
            {
                return this._valid;
            }
            set
            {
                this._valid = value;
                if (this._valid)
                {
                    this._errorText = "";
                }
            }
        }
    }
   
}
