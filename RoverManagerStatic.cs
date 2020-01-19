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
        //not all return a value ... but they do? - intellisense does not recognise some routes not possibe


        //command string is wrong word now maybe roverManagerCommandString - call substring selectedRoverCommandString
        public static RoversTasksValidation TryThenRunCommandString(string commandString)
        {

            //could i use int t for test loop int e for execture loop

            RoversTasksValidation roverResponse = new RoversTasksValidation();
            string roverCommandSubString = "";

            //****************************************************************     Test routes   *********************


            for (int i = 0; i < commandString.Length; i++)
            {
                //if your have a command to run and you have either got to the last command so it is to be applied to the last rover or the rover has been just changed then give command to last rover
                if (RoverDictionary.ContainsKey(commandString[i].ToString()))
                {

                    //if we get a new rover we can execture the string of the last
                    //if there is a string to execicute
                    if (roverCommandSubString.Length > 0)
                    {

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
                    SelectedRover = RoverDictionary[commandString[i].ToString()];
                    roverCommandSubString = "";
                    roverResponse = SelectedRover.validateRouteOfCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(roverCommandSubString));//this line is so get report just for changing rover
                    

                    //if we are at the end of the command sequence then assign the subString to the current rover

                }
                else if (StaticMoveCommandFactoryDic.commandKeys.ContainsKey(commandString[i].ToString()) && (i == commandString.Length - 1))
                {
                    //this is the last command so add it to the string and then run it
                    roverCommandSubString += commandString[i].ToString();
                    if ((roverResponse = SelectedRover.validateRouteOfCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(roverCommandSubString))).CommandsExecutionSuccess == false)
                    {
                        //if this string fails then return failure
                        return roverResponse; //this stops the process and tells the user where it went wrong
                    }
                }


                //if the command character is command rather than a rover use it to build the string

                //to protect errors should we ensure it is not last 
                //to protect errors should we check it is not also in the rover dictionary
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


                //*************************************     EXECUTE       ****************************


                //if you test all rovers and get here then
                //execute them all
                if (i == commandString.Length - 1)
                {
                    roverCommandSubString = "";
                    for (int j = 0; j < commandString.Length; j++)
                    {



                       

                        //if your have a command to run and you have either got to the last command so it is to be applied to the last rover or the rover has been just changed then give command to last rover
                        if (RoverDictionary.ContainsKey(commandString[j].ToString()))
                        {

                            //if we get a new rover we can execture the string of the last
                            //if there is a string to execicute
                            if (roverCommandSubString.Length > 0)
                            {

                                roverResponse = SelectedRover.ExecuteCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(roverCommandSubString));

                            }
                            SelectedRover = RoverDictionary[commandString[j].ToString()];
                            
                            roverResponse = SelectedRover.ExecuteCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(roverCommandSubString = ""));
                            
                            //dont understand this error to reproduce it type create = c name = p minx 0 miny 0 maxx 10 maxy 10 start x 5 start y 5 facing n, then type ffffpffffff
                            
                            //is this causing it run twice
                            //why is the below code running before roverCommandSubString
                            //roverResponse = SelectedRover.ExecuteCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdLicst(roverCommandSubString)); //just so if change rover by just putting its name get a reportc
                            //validate right at the end
                            //if we are at the end of the command sequence then assign the subString to the current rover

                        }
                        //dont need to check commandstring length because about to increase it
                        else if (StaticMoveCommandFactoryDic.commandKeys.ContainsKey(commandString[j].ToString()) && (j == commandString.Length - 1))
                        {
                            //this is the last command so add it to the string and then run it
                            roverCommandSubString += commandString[j].ToString();

                            roverResponse = SelectedRover.ExecuteCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(roverCommandSubString));
                            roverCommandSubString = ""; //we should not go through code anymore so should not be necessary
                        }


                        //if the command character is command rather than a rover use it to build the string

                        //to protect errors should we ensure it is not last 
                        //to protect errors should we check it is not also in the rover dictionary
                        else if (StaticMoveCommandFactoryDic.commandKeys.ContainsKey(commandString[j].ToString()))
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

                }
            }
            //if you left the for loop without returning a rover response eg last instruction was to change rover then return it now
            //should only get her if tests have passed
            return roverResponse;
        }
    }
}
