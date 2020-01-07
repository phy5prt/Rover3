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
            string userInput = "";
            while ((userInput = consoleHandler.getUserInput()) != "Q")
            {
                //Select Rover or Create rover
                //Input Command for that Rover
                string commandInput = userInput; // later different inputs depending which dic of commands
                consoleHandler.displayText(CheckProcessUserCommandInput(commandInput));
            }
       

            //need string input to IList
            // rover.runCommandSequence(consoleHandler.getUserInput());


        }
        public string InitialMessage() {
            string initialInstructions = "Currently the program automatically creates a single rover facing south at coord 0 10 on a grid of 0-10 in X-Y directions. Press Q to quit. Press command keys to make a command string to be executed. Valid keys are :";
            return initialInstructions;
        }
        public string Instructions() {
            return string.Join(" ", StaticCommandFactoryDic.commandKeys.Keys.ToArray());
  
        }
        public ResultOfCommandSequenceValidation CheckInputValid(String commandString) {
            ResultOfCommandSequenceValidation resultOfCommandSequenceValidation = new ResultOfCommandSequenceValidation();
            resultOfCommandSequenceValidation.errorText += "The following commands were not recognised: ";
            resultOfCommandSequenceValidation.valid = true;



            for (int i = 0; i < commandString.Length; i++)
            {

                if (!StaticCommandFactoryDic.commandKeys.ContainsKey(commandString[i].ToString()))
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
                command = StaticCommandFactoryDic.commandKeys[userInputKey];
                userCommandList.Add(command);
            }
            return userCommandList;
        }

        public string CheckProcessUserCommandInput(string userInput) {

           

            ResultOfCommandSequenceValidation resultOfCommandSequenceValidation = new ResultOfCommandSequenceValidation();
            resultOfCommandSequenceValidation = CheckInputValid(userInput);
            CommandSequenceExecutableValidation commandSequenceExecutableValidation = new CommandSequenceExecutableValidation();
            string errorNoLocationChange = " The rover has not been moved please input a valid command string.";
            string successfulCommandExectutionTxt = " The rover has successfully been moved.";

            if (!resultOfCommandSequenceValidation.valid) { return resultOfCommandSequenceValidation.errorText + errorNoLocationChange + ReportLocation(); }
            else {
               IList<Command> userCommandList = UserInputToCommands(userInput);
                commandSequenceExecutableValidation=rover.runCommandSequence(userCommandList);
                if (commandSequenceExecutableValidation.CommandsExecutionSuccess)
                {
                    return successfulCommandExectutionTxt + ReportLocation();

                }
                else {
                    //differentiating from out of bound or object or rover would be good
                    // telling them the bounds would be good to 
                    string errorUnableToExecuteCommands = "The command sequence is invalid. It could not be exectued at : ";
                    errorUnableToExecuteCommands += userInput.Insert(commandSequenceExecutableValidation.InvalidCommandIndex, "*").Insert(commandSequenceExecutableValidation.InvalidCommandIndex + 2, "*");
                    errorUnableToExecuteCommands +=  string.Format(" because it would be out of bounds at X = {0} and Y = {1}.", commandSequenceExecutableValidation.WhereLocationBecomesInvalid.XCoord.ToString(), commandSequenceExecutableValidation.WhereLocationBecomesInvalid.YCoord.ToString());
                    errorUnableToExecuteCommands += errorNoLocationChange;
                    return errorUnableToExecuteCommands;


                }

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
