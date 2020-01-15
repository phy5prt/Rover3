using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    static class RoverManagerStatic
    {
        //another change
        //here is a change
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

        public RoverTaskValidation TryThenRunCommandString(string commandString){
            RoverTasksValidation roverResponse;
            string roverCommandSubString;
            //Test routes
        for(int i = 0; i<commandString.Length; i++){
            
                if(   
                    //if your have a command to run and you have either got to the last command so it is to be applied to the last rover or the rover has been just changed then give command to last rover
                    (
                       RoverDictionary.contains.keys(commandString[i])
                       || 
                       i ==  commandString.Length -1 ) 
                       && roverCommandSubString.Length>0){
                  
                        //if we get a new rover we can execture the string of the last
                        if((roverResponse = SelectedRover.validateRouteOfCommandSequence(roverCommandSubString).CommandsExecutionSuccess) == false)
                        {   
                            //This will not work properly because it needs to be in the context of the whole string
                            //options are to return information to rover manager and rover manager make a report from previous ones 
                            //Or we just change the report to say string command failed because rover X would not of been able to complete command because ...
                            //or we make a list of response and send that back 
                            // though if rover x would of blocked y this would be a problem
                            //So maybe build the report up to the error or just add on the string that was cut off before this rovers <-- probably easiest best fix
                            return roverResponse; //this stops the process and tells the user where it went wrong
                        } 
                    roverCommandSubstring=""; 
                    SelectedRover = RoverDictionary[commandString[i]];}
                else if (RoverManagerStatic.contains.keys(commandString[i]))
                           {
                            roverCommandSubString += commandString[i];
               
                }
                //if you test all rovers and get here then
                 if(i ==  commandString.Length -1){
                 roverCommandSubString = "";

                   for(int i = 0; i<commandString.Length; i++){
               
                   if((
                    RoverDictionary.contains.keys(commandString[i])
                       || 
                       i ==  commandString.Length -1 ) 
                       && roverCommandSubString.Length>0){
                  
                        //if we get a new rover we can execture the string of the last
                        if((roverResponse = SelectedRover.validateRouteOfCommandSequence(roverCommandSubString).CommandsExecutionSuccess) == false)
                        {   
                            //This will not work properly because it needs to be in the context of the whole string
                            //options are to return information to rover manager and rover manager make a report from previous ones 
                            //Or we just change the report to say string command failed because rover X would not of been able to complete command because ...
                            //or we make a list of response and send that back 
                            // though if rover x would of blocked y this would be a problem
                            //So maybe build the report up to the error or just add on the string that was cut off before this rovers <-- probably easiest best fix
                            return roverResponse; //this stops the process and tells the user where it went wrong
                        } 
                    roverCommandSubstring=""; 
                    SelectedRover = RoverDictionary[commandString[i]];}
                
                
                }

            }

        //Run routes

        }
        public void RoversCheckRoute(){}
        public void RoversEnactCommands(){}
    }
}
