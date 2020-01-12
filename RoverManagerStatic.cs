using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    static class RoverManagerStatic
    {
        public static Rover SelectedRover { get; set; }

        //may need delete function
        public static Dictionary<string, Rover> RoverDictionary = new Dictionary<string, Rover> { };

        public static void AddRoverToRoverDictionary(Rover roverToAdd)
        { //couldnt overide dictionary as isnt virtual

            RoverDictionary.Add(roverToAdd.RoverKeyName, roverToAdd);
            //instead of user interface make the added rover the selected rover I could do it here
            //which is best
            //ussually the user selected the rover currentlly userinterface does it
            //if later user interface just give the command to rover manager then 
            //rover manager should change own selected rover
        }
    }
}
