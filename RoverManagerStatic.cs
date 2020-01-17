using Rover3.MoveCommands;
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
        //not all return a value ... but they do?
        public static RoversTasksValidation TryThenRunCommandString(string commandString)
        {

            RoversTasksValidation roverResponse = new RoversTasksValidation();
            string roverCommandSubString = "";

            //Test routes
            for (int i = 0; i < commandString.Length; i++ )
            {
                //if your have a command to run and you have either got to the last command so it is to be applied to the last rover or the rover has been just changed then give command to last rover
                if ((RoverDictionary.ContainsKey(commandString[i].ToString())
                {

                    //if we get a new rover we can execture the string of the last
                    //should rove turn string to commands?>
                    if ((roverResponse = SelectedRover.validateRouteOfCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(roverCommandSubString))).CommandsExecutionSuccess == false)
                    {
                        //This will not work properly because it needs to be in the context of the whole string
                        //options are to return information to rover manager and rover manager make a report from previous ones 
                        //Or we just change the report to say string command failed because rover X would not of been able to complete command because ...
                        //or we make a list of response and send that back 
                        // though if rover x would of blocked y this would be a problem
                        //So maybe build the report up to the error or just add on the string that was cut off before this rovers <-- probably easiest best fix
                        return roverResponse; //this stops the process and tells the user where it went wrong
                    }
                    SelectedRover = RoverDictionary[commandString[i].ToString()];
                    roverCommandSubString = "";

                } else if ((i == commandString.Length - 1) && (commandString.Length > 0)) {
                    if ((roverResponse = SelectedRover.validateRouteOfCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(roverCommandSubString))).CommandsExecutionSuccess == false)
                    {
                        //This will not work properly because it needs to be in the context of the whole string
                        //options are to return information to rover manager and rover manager make a report from previous ones 
                        //Or we just change the report to say string command failed because rover X would not of been able to complete command because ...
                        //or we make a list of response and send that back 
                        // though if rover x would of blocked y this would be a problem
                        //So maybe build the report up to the error or just add on the string that was cut off before this rovers <-- probably easiest best fix
                        return roverResponse; //this stops the process and tells the user where it went wrong
                    }
                }
                else if (StaticMoveCommandFactoryDic.commandKeys.ContainsKey(commandString[i].ToString()))
                {
                    roverCommandSubString += commandString[i].ToString();
                }
                else
                {
                    //we dont want a rover repsonse we want an error
                    //but it is expecting a roverResponse
                    return roverResponse;
                }




                //if you test all rovers and get here then
                if (i == commandString.Length - 1)
                {

                    roverCommandSubString = "";

                    for (int j = 0; i < commandString.Length; j++)
                    {

                        
                        if ((RoverDictionary.ContainsKey(commandString[j].ToString()) || j == commandString.Length - 1) && commandString.Length > 0)

                        {

                            //if we get a new rover we can execture the string of the last
                           //we do this before changing which rover we are using
                            roverResponse = SelectedRover.ExecuteCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(roverCommandSubString));
                            roverCommandSubString = "";
                            SelectedRover = RoverDictionary[commandString[j].ToString()];
                            //return roverResponse; //we may want to collect  rover response here instead add it to a list
                        }
                    
                        else if (RoverManagerStatic.RoverDictionary.ContainsKey(commandString[j].ToString()))
                        {
                            roverCommandSubString += commandString[j].ToString();
                        }
                        else
                        {
                            //we dont want a rover repsonse we want an error
                            //but it is expecting a roverResponse
                            return roverResponse;
                        }

                     }
                    //but this is just the last rover
                    return roverResponse;

                }
                //Should never get here
                //but need to put a return to keep the compiler happu
                return roverResponse;
            }
           
            //Should never get here
            //but need to put a return to keep the compiler happu
            return roverResponse;
        }

    }
}
