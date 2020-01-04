using Rover3.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover3
{
    class UserInterface
    {
        ConsoleHandler consoleHandler = new ConsoleHandler();


        //Later should request start location of rover 
        //private Orientation StartSouth = new South();
        //private int   initXCoord=0, initYCoord=0, xLowBound = 0, xHighBound = 10, yLowBound = 0, yHighBound = 10;

        //LocationInfo roverInitLocation = new LocationInfo(StartSouth, initXCoord, initYCoord, xLowBound, xHighBound, yLowBound, yHighBound );
      

       

        Rover rover = new Rover(new LocationInfo(new South(), 0, 0, 0, 10, 0, 10));//later make this a key press to add a rover to the a list of rovers accessible with a number
        public UserInterface() {
            consoleHandler.displayText(InitialMessage());
            consoleHandler.displayText(Instructions());
            consoleHandler.displayText(ReportLocation());
            consoleHandler.displayText( CheckProcessUserInput(consoleHandler.getUserInput()));
            consoleHandler.displayText(ReportLocation()); 

            //need string input to IList
            // rover.runCommandSequence(consoleHandler.getUserInput());


        }
        public string InitialMessage() {
            string initialInstructions = "Currently the program automatically creates a single rover facing south at coord 0 10 on a grid of 0-10 in X-Y directions. Press Q to quit. Press command keys to make a command string to be executed. Valid keys are :";
            return initialInstructions;
        }
        public string Instructions() {
            return string.Join(" ", CommandKeyDictionary.commandKeys.Keys.ToArray());
  
        }
        public ResultOfCommandSequenceValidation CheckInputValid(String commandString) {
            ResultOfCommandSequenceValidation resultOfCommandSequenceValidation = new ResultOfCommandSequenceValidation();
            resultOfCommandSequenceValidation.errorText += "The following commands were not recognised: ";
            resultOfCommandSequenceValidation.valid = true;



            for (int i = 0; i < commandString.Length; i++)
            {

                if (!CommandKeyDictionary.commandKeys.ContainsKey(commandString[i].ToString()))
                {
                    commandString = commandString.Insert(i, "*").Insert(i + 2, "*");
                    resultOfCommandSequenceValidation.errorText += commandString[i+1] + " ";
                    resultOfCommandSequenceValidation.valid = false;
                    i += 2;

                }

                               
            }

        

            if (resultOfCommandSequenceValidation.valid) { resultOfCommandSequenceValidation.errorText = ""; } else { resultOfCommandSequenceValidation.errorText += commandString; }
            return resultOfCommandSequenceValidation;
        
        }
        public void InteractWithUser()
        {

        }
        public IList<Command> UserInputToCommands(string userInput) {
            IList<Command> userCommandList = new List<Command>();
            Command command;
            for (int i = 0; i<userInput.Length; i++) {
                string userInputKey = userInput[i].ToString();
                command = CommandKeyDictionary.commandKeys[userInputKey];
                userCommandList.Add(command);
            }
            return userCommandList;
        }

        public string CheckProcessUserInput(string userInput) {
            ResultOfCommandSequenceValidation resultOfCommandSequenceValidation = new ResultOfCommandSequenceValidation();
            resultOfCommandSequenceValidation = CheckInputValid(userInput);
            string errorNoLocationChange = " The rover has not been moved please input a valid command string.";

            if (!resultOfCommandSequenceValidation.valid) { return resultOfCommandSequenceValidation.errorText + errorNoLocationChange + ReportLocation(); }
            else {
               IList<Command> userCommandList = UserInputToCommands(userInput);
                return rover.runCommandSequence(userCommandList);

            }
        }

        public string ReportLocation() {

            string LocationReport = "Current rover location: ";
            LocationReport += "x location is ";
            LocationReport += rover.currentLocation.XCoord.ToString() + ". ";
            LocationReport += "y location is ";
            LocationReport += rover.currentLocation.YCoord.ToString() + ". ";

            return LocationReport;
        }
        

    }
}
