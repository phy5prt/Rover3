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
        public static bool RemoveRoverFromDictionary(String roverName) 
        {
            bool removalSucceeded = false;
            if (RoverDictionary.ContainsKey(roverName)) 
            { 
                    RoverDictionary.Remove(roverName);
                    removalSucceeded = true; 
            }
            return removalSucceeded;
        }


        //qqqqq
        public static IList<RoverTasksValidation> ValidateCommandStringRouteAndRun(string fullCommandStr)
        {

            string selectedRoverBeforeValidation = SelectedRover.RoverKeyName;

            IList<string> roversCommandStrLs = new List<string>();
            //command result or something that has a list of rover task validations, tracks the command, and a fail bool rather than giving the last rover the commandstr
            IList<RoverTasksValidation> roversResponses = new List<RoverTasksValidation>();
            //for location can check rovers weve validated for test location and ones we havent for actual

            StringBuilder commandStringSegmentSB = new StringBuilder(50);

            //if user presses return give them current location last used rover //qqqq should i just return it no command sequence etc //do i need to do the same for substrings length zero so if you put in name of a rover you get a report and a list of rovers a list of reports
            if (fullCommandStr.Length == 0) { roversResponses.Add(SelectedRover.validateRouteOfCommandSequence(MoveCommandDicManager.MoveCommandStrToCmdList(fullCommandStr))); }

            //make roversCommandStrLs
            for (int i = 0; i < fullCommandStr.Length; i++)
            {


                if (RoverDictionary.ContainsKey(fullCommandStr[i].ToString()))
                {
                    if (commandStringSegmentSB.Length > 0)
                    {
                        //current roverTask string has finished add it to list and start the next
                        roversCommandStrLs.Add(commandStringSegmentSB.ToString());
                    }
                    commandStringSegmentSB.Clear().Append(fullCommandStr[i].ToString());
                }
                else
                {
                    commandStringSegmentSB.Append(fullCommandStr[i].ToString());
                }
                //add if its the end of te string
                if (i == fullCommandStr.Length - 1)
                {
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
                if ( (roversResponses[roversResponses.Count-1].CommandsExecutionSuccess == false))
                {
                    //converting invalid index from single rover command to full set of rover commands
                    roversResponses[roversResponses.Count-1].InvalidCommandIndex += fullCommandStrIndex;
                    //reset rovers --

                    foreach (RoverTasksValidation task in roversResponses) { RoverDictionary[task.NameOfRover].RevertTestRoverToCurrentLocation(); }
                    SelectedRover = RoverDictionary[selectedRoverBeforeValidation];
                    return roversResponses;
                }
                fullCommandStrIndex += roverCommandStr.Length;




            }
            //all passed reset the test and allow them to execute
            foreach (RoverTasksValidation task in roversResponses) { RoverDictionary[task.NameOfRover].RevertTestRoverToCurrentLocation(); }
            SelectedRover = RoverDictionary[selectedRoverBeforeValidation];
            ExecuteCommandString(roversCommandStrLs);
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


        private static void ExecuteCommandString(IList<string> roversCommandStrLs)
        {


            //execute routes
            for (int i = 0; i < roversCommandStrLs.Count; i++)
            {
                //if it changes rover 
                string roverCommandStr = roversCommandStrLs[i];
                if (RoverDictionary.ContainsKey(roverCommandStr[0].ToString()))
                {
                    SelectedRover = RoverDictionary[roverCommandStr[0].ToString()];
                    roverCommandStr = roverCommandStr.Substring(1);
                }

                SelectedRover.ExecuteCommandSequence(MoveCommandDicManager.MoveCommandStrToCmdList(roverCommandStr));

            }
        }

    }
}

