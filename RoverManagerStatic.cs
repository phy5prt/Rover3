using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{

    /*
     Future delete reover function for the dictionary
     Changes:
     fullCommandStr   --- the full string
     roverComandStr   --- a section of inputStr for a rover (the same rover can be called more than once in a fullCommandStr ) 
     Redesign seperate validate and execution
     roverReport everytime change rover and for last rover
     create a list of unavailable coordinates e.g x=0 y=5 is occupied = true occupee = RoverA
         

        --cut string
        --bool for group one fail all fail
        --

         */
    static class RoverManagerStatic
    {

        public static Rover SelectedRover { get; set; }

        public static Dictionary<string, Rover> RoverDictionary = new Dictionary<string, Rover> { };

        public static void AddRoverToRoverDictionary(Rover roverToAdd)
        { //couldnt overide dictionary as isnt virtual

            RoverDictionary.Add(roverToAdd.RoverKeyName, roverToAdd);
            //instead of user interface make the added rover the selected rover I should do it here
            //user interface just give the command to rover manager then 
            //rover manager should change own selected rover
        }


        //qqqqq
        public static IList<RoverTasksValidation> ValidateCommandStringRouteAndRun(string fullCommandStr) {
            
            IList<string> roversCommandStrLs = new List<string>();
            //command result or something that has a list of rover task validations, tracks the command, and a fail bool rather than giving the last rover the commandstr
            IList<RoverTasksValidation> roversResponses = new List<RoverTasksValidation>();
            //for location can check rovers weve validated for test location and ones we havent for actual

            StringBuilder commandStringSegmentSB = new StringBuilder(50);
            
            //make roversCommandStrLs
            for (int i = 0; i < fullCommandStr.Length; i++) {


                if (RoverDictionary.ContainsKey(fullCommandStr[i].ToString())){
                    if (commandStringSegmentSB.Length > 0) {
                        //current roverTask string has finished add it to list and start the next
                        roversCommandStrLs.Add(commandStringSegmentSB.ToString());
                     }
                    commandStringSegmentSB.Clear().Append(fullCommandStr[i].ToString());
                }
                else {
                    commandStringSegmentSB.Append(fullCommandStr[i].ToString());
                }
                //add if its the end of te string
                if (i == fullCommandStr.Length - 1) { 
                    roversCommandStrLs.Add(commandStringSegmentSB.ToString());
                    commandStringSegmentSB.Clear();
                }   
            }
            //validate routes
            int fullCommandStrIndex = 0;
            for (int i = 0; i < roversCommandStrLs.Count; i++)
            {

                string roverCommandStr = roversCommandStrLs[i];
                if (RoverDictionary.ContainsKey(roverCommandStr[0].ToString()))
                {
                    fullCommandStrIndex++;
                    SelectedRover = RoverDictionary[roverCommandStr[0].ToString()];
                    roverCommandStr = roverCommandStr.Substring(1);
                }
                roversResponses.Add(SelectedRover.validateRouteOfCommandSequence(MoveCommandDicManager.MoveCommandStrToCmdList(roverCommandStr)));
                //stop validating if route not possible and return
                if (roversResponses[roversResponses.Count].CommandsExecutionSuccess == false)
                {
                    //converting invalid index from single rover command to full set of rover commands
                    roversResponses[roversResponses.Count].InvalidCommandIndex += fullCommandStrIndex;
                    //reset rovers --

                    foreach (RoverTasksValidation task in roversResponses) { RoverDictionary[task.NameOfRover].RevertTestRoverToCurrentLocation(); }
                    return roversResponses;
                }
                fullCommandStrIndex += roverCommandStr.Length;




            }
            //all passed reset the test and allow them to execute
            foreach (RoverTasksValidation task in roversResponses) { RoverDictionary[task.NameOfRover].RevertTestRoverToCurrentLocation(); }
            ExecuteCommandString(fullCommandStr);
            return roversResponses;



        }












        //if rovers all pass the executeCommandString should set 
        //a list of locations matching the taskValidation set of locations
        //it should put these into the rovers so they have a history +++ maybe in the get set whenever they receive a new location the last one is added to a list
        //executeCommandString should return its list
        //or not --- validate should validate wont hit other rovers
        //but execute should then move them and depending what they find things should happen. 
        //execute should run them one at a time, they scan dig etc whilst moving (get around issue of what if one is busy dont want time to be an issue
       //time move for 10 minutes, turning is instant testing is also ten minutes. --- rovers not in same square as risk crash
       //need to design

        private static void ExecuteCommandString(string fullCommandStr) {

            string roverCommandStr = "";

            for (int j = 0; j < fullCommandStr.Length; j++)
            {


                //if your have a command to run and you have either got to the last command so it is to be applied to the last rover or the rover has been just changed then give command to last rover
                if (RoverDictionary.ContainsKey(fullCommandStr[j].ToString()))
                {

                    //if we get a new rover we can execture the string of the last
                    //if there is a string to execicute
                    if (roverCommandStr.Length > 0)
                    {

                        //roverResponse.Add(
                        SelectedRover.ExecuteCommandSequence(MoveCommandDicManager.MoveCommandStrToCmdList(roverCommandStr));
                        //);

                    }
                    SelectedRover = RoverDictionary[fullCommandStr[j].ToString()];
                    roverCommandStr = "";
                    //qqqq why do we add a rover response to executing
                    //roverResponse.Add(
                    SelectedRover.ExecuteCommandSequence(MoveCommandDicManager.MoveCommandStrToCmdList(roverCommandStr));
                    //);

                    //dont understand this error to reproduce it type create = c name = p minx 0 miny 0 maxx 10 maxy 10 start x 5 start y 5 facing n, then type ffffpffffff

                    //is this causing it run twice
                    //why is the below code running before roverCommandStr
                    //roverResponse = SelectedRover.ExecuteCommandSequence(StaticMoveCommandFactoryDic.MoveCommandStrToCmdLicst(roverCommandStr)); //just so if change rover by just putting its name get a reportc
                    //validate right at the end
                    //if we are at the end of the command sequence then assign the subString to the current rover

                }
                //dont need to check fullCommandStr length because about to increase it
                else if (MoveCommandDicManager.KeyExistsInAMoveCommandDictionary(fullCommandStr[j].ToString()) && (j == fullCommandStr.Length - 1)) 
                {
                    //this is the last command so add it to the string and then run it
                    roverCommandStr += fullCommandStr[j].ToString();
                    //qqqq we add here
                    //roverResponse.Add(
                    SelectedRover.ExecuteCommandSequence(MoveCommandDicManager.MoveCommandStrToCmdList(roverCommandStr));
                        //);
                    roverCommandStr = ""; //we should not go through code anymore so should not be necessary
                }


                //if the command character is command rather than a rover use it to build the string

                //to protect errors should we ensure it is not last 
                //to protect errors should we check it is not also in the rover dictionary
                else if (MoveCommandDicManager.KeyExistsInAMoveCommandDictionary(fullCommandStr[j].ToString()))
                {
                    roverCommandStr += fullCommandStr[j].ToString();
                }
                else
                {
                    //we dont want a rover repsonse we want an error
                    //but it is expecting a roverResponse
                    Console.WriteLine("Shouldnt be here - error");
                    //return roverResponse;
                }


            }


            
        }
    }
}

