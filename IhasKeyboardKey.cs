using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    interface IKeyboardKey
    {
        public abstract string Key { get; }
        public abstract string KeyFunctionDescription { get; }
    }
}
