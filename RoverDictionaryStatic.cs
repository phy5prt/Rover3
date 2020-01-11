using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    static class RoverDictionaryStatic
    {
        //may need delete function
        public static Dictionary<string, Rover> RoverDictionary = new Dictionary<string, Rover> { };

        public static void addRoverToRoverDictionary(Rover roverToAdd) { //couldnt overide dictionary as isnt virtual

            RoverDictionary.Add(roverToAdd.RoverKeyName, roverToAdd);
        }
    }
}
