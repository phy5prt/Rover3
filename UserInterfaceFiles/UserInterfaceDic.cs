using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3.UserInterfaceFiles
{
    public static class UserInterfaceDic
    {
        public static IDictionary<string, InterfaceKey> interfaceDic = new Dictionary<string, InterfaceKey>()
        {
            //Destroy rover should be available when moving put with scanning science commands
          
            { new C().Key, new C()},
            { new K().Key, new K()}, 
            { new U().Key, new U()},
            { new Q().Key, new Q()},
            { new D().Key, new D()}

        };
    }
}
