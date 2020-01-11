using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover3
{
    class UserInterface
    {
        ConsoleHandler consoleHandler = new ConsoleHandler(); //only ever 1 make static?


        //Later should request start location of rover 
        //private Orientation StartSouth = new South();
        //private int   initXCoord=0, initYCoord=0, xLowBound = 0, xHighBound = 10, yLowBound = 0, yHighBound = 10;

        //LocationInfo roverInitLocation = new LocationInfo(StartSouth, initXCoord, initYCoord, xLowBound, xHighBound, yLowBound, yHighBound );




        Rover rover; //later this will be A dictionary static of rovers
      //later make this a key press to add a rover to the a list of rovers accessible with a number
        public UserInterface() {
            consoleHandler.DisplayText(InitialMessage());
            consoleHandler.DisplayText(Instructions());
            consoleHandler.DisplayText(ReportLocation());
            string userInput = "";
            while ((userInput = consoleHandler.GetUserInput()) != "Q")
            {
                
                //if no rover set ask for rover if there are some, or to create one if not
                //Select Rover or Create rover
                //If S select rover
                //If C create rover + set as selected rover
                //IF D destroy current rover 
                //Input Command for that Rover
                string commandInput = userInput; // later different inputs depending which dic of commands
                consoleHandler.DisplayText(CheckProcessUserCommandInput(commandInput));
            }
       

            //need string input to IList
            // rover.runCommandSequence(consoleHandler.getUserInput());


        }
        public string InitialMessage() {
            string initialInstructions = "Currently the program automatically creates a single rover facing south at coord 0 10 on a grid of 0-10 in X-Y directions. Press Q to quit. Press command keys to make a command string to be executed. Valid keys are :";
            return initialInstructions;
        }
        public string Instructions() {
            return string.Join(" ", StaticMoveCommandFactoryDic.commandKeys.Keys.ToArray());
  
        }
        public ResultOfCommandSequenceValidation CheckInputValid(String commandString) {
            ResultOfCommandSequenceValidation resultOfCommandSequenceValidation = new ResultOfCommandSequenceValidation();
            resultOfCommandSequenceValidation.errorText += "The following commands were not recognised: ";
            resultOfCommandSequenceValidation.valid = true;



            for (int i = 0; i < commandString.Length; i++)
            {

                if (!StaticMoveCommandFactoryDic.commandKeys.ContainsKey(commandString[i].ToString()))
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

        //rearrange text order
        public Rover CreateNewRover() {
            //Should i be making each information grab its own method, if so method in a method, or methods along side
            int newRoverXCoord, newRoverYCoord, newRoverXMin, newRoverXMax, newRoverYMin, newRoverYMax;

            Orientation newRoverOrientation; // this does work because have no was of setting an orientation just of applying one 
            //to a current locationInfor
            //therefore will need to make a location Information and then apply the command face east to overide our default
            //this may be bad design so maybe I should be setting all orientations based on degrees or radians starting north

            //We have to initialise a location with default values and overwrite it because we can over write an orientation but we have now way of just making one


            string newRoverKey;
            string userInput = "";

            //What if they want to quit during setup
            //Validation

            //Chose a unique single key to assign to your rover

            consoleHandler.DisplayText("During rover creation you cannot use the q, moveCommand keys or create destroy select keys. The newly created rover will be set as the currently selected rover. To set up the rover you will be asked for a single unique key name for it, its current coordinates and its maximum allowed X and Y position as well as it's starting orientation");
            string keysAlreadyInUse = Instructions() + " Q S C D "; // later needs to be moveCommand + interfaceCommands
            bool validInput;
            do
            {
                consoleHandler.DisplayText("Enter a single unique key to select your rover with.");
                validInput = !StaticMoveCommandFactoryDic.commandKeys.ContainsKey((newRoverKey = consoleHandler.GetUserInput())) || newRoverKey == "Q" || newRoverKey == "S" || newRoverKey == "C" || newRoverKey == "D";

                if (!validInput) { consoleHandler.DisplayText(newRoverKey + " : " + " is not a unique key"); }
                else { consoleHandler.DisplayText("When the new rover is setup it will be call " + newRoverKey + " when you press the " + newRoverKey + " it will be selected "); }
            }
            while (!validInput);

            //Chose your boundaries

            consoleHandler.DisplayText("You will now choose the area your rover is allowed to move within by setting a min and max for X and Y coordinates. These numbers are inclusive.");
            //If get boundaries mixed up need a key to allow them to reset that section 


            //Choose your Xmin
            do
            {
                consoleHandler.DisplayText("Please choose the minimum X position you want your rover to be able to go to");

                userInput = consoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverXMin);
                if (!validInput) { consoleHandler.DisplayText(userInput + " is not a valid integer "); }
                else { consoleHandler.DisplayText("X minimum boundary will be set to " + newRoverXMin + "when the rover is created."); }
            }
            while (!validInput);


            //Choose your  YMin

            do
            {
                consoleHandler.DisplayText("Please choose the minimum Y position you want your rover to be able to go to");

                userInput = consoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverYMin);
                if (!validInput) { consoleHandler.DisplayText(userInput + " is not a valid integer "); }
                else { consoleHandler.DisplayText("Y minimum boundary will be set to " + newRoverYMin + " when the rover is created."); }
            }
            while (!validInput);

            //Choose your  XMax

            do
            {
                consoleHandler.DisplayText("Please choose the maximum X position you want your rover to be able to go to");

                userInput = consoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverXMax);
                if (!validInput) { consoleHandler.DisplayText(userInput + " is not a valid integer ");

                } else if(newRoverXMax >= newRoverXMin)
                    {
                    consoleHandler.DisplayText(userInput + " the maximum X boundary must be greater than or equal to the minimum. The min boundary is " + newRoverXMin.ToString()); // here will need option to reset min
                }
                
                else { consoleHandler.DisplayText("X maximum boundary will be set to " + newRoverXMax + " when the rover is created."); }
            }
            while (!validInput);

            //Choose your  YMax

            do
            {
                consoleHandler.DisplayText("Please choose the maximum Y position you want your rover to be able to go to");

                userInput = consoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverYMax);
                if (!validInput)
                {
                    consoleHandler.DisplayText(userInput + " is not a valid integer ");

                }
                else if (newRoverYMax >= newRoverYMin)
                {
                    consoleHandler.DisplayText(userInput + " the maximum X boundary must be greater than or equal to the minimum. The min boundary is " + newRoverXMin.ToString()); // here will need option to reset min
                }

                else { consoleHandler.DisplayText("X maximum boundary will be set to " + newRoverYMax + " when the rover is created."); }
            }
            while (!validInput);


            //Chose your coords

            consoleHandler.DisplayText("You will now choose your starting coords for your new rover. " +
                "They must be within or equal to the range values you set for X and Y. " +
                " For X that is " + newRoverXMin.ToString()+"-"+ newRoverXMax.ToString()+ 
                " For Y that is " + newRoverYMin.ToString() +" - "+ newRoverYMax.ToString());

            //Chose your XCoord

            do
            {
                consoleHandler.DisplayText("Please choose an initial X coordinate value for the rover.");
                
                userInput = consoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverXCoord);
                if (!validInput) { consoleHandler.DisplayText(userInput + " is not a valid integer "); }
                else if (newRoverXCoord < newRoverXMin || newRoverXCoord > newRoverXMax) 
                {
                    consoleHandler.DisplayText("The give X coord " + newRoverXCoord.ToString() + " is not within the X bounds of " + newRoverXMin.ToString() + "-" + newRoverXMax.ToString()); 
                        } 
                else { consoleHandler.DisplayText("X coord will be set to " + newRoverXCoord + "when the rover is created."); 
            }
            }
            while (!validInput);


            //Chose your YCoord

            do
            {
                consoleHandler.DisplayText("Please choose an initial Y coordinate value for the rover");
                userInput = consoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverYCoord);
                if (!validInput) { consoleHandler.DisplayText(userInput + " is not a valid integer "); }
                else if (newRoverYCoord < newRoverYMin || newRoverYCoord > newRoverYMax)
                {
                    consoleHandler.DisplayText("The give Y coord " + newRoverYCoord.ToString() + " is not within the Y bounds of " + newRoverYMin.ToString() + "-" + newRoverYMax.ToString());
                }
                else { consoleHandler.DisplayText("Y coord will be set to " + newRoverYCoord + "when the rover is created."); }
            }
            while (!validInput);






            //Chose initial orientation for your rover
            //I cant use this :
            //string.Join(" ", StaticMoveCommandFactoryDic.commandKeys.Keys.ToArray());
            //because it would include the movement commands
            //so if I wanted to create a ne orientation i would have to alter this code
            //which suggests they should be two different dictionaries
            //or that each method should hold its own description of what it does and the interface can the just get the keys
            //and the description and put them up
            //best solution is seperate dictionary but still good idea to have a description in the commands

            consoleHandler.DisplayText("Please choose an initial starting orientation for your rover");

            do
            {
                consoleHandler.DisplayText("Enter a starting orientation for your rover it must be one of N E S W."); // later should come from the orientation dictionary 
                userInput = consoleHandler.GetUserInput();
                validInput = (userInput == "N"||userInput=="E"||userInput=="S"||userInput=="W"); 

                if (!validInput) { consoleHandler.DisplayText(userInput + " : " + " is not a valid orientation"); }
                else {
                    newRoverOrientation = StaticMoveCommandFactoryDic.commandKeys.[userInput];

                    consoleHandler.DisplayText("When the new rover is setup it will have the orientation " + userInput); 
                }
            }
            while (!validInput);



            //dictionary should create and return the rover the userInterface should just get the info

            Rover newRover = new Rover(new LocationInfo(new South(), 0, 0, 0, 10, 0, 10));
            //add new rover to dictionary
            return newRover;

        }
        public IList<MoveCommand> UserInputToCommands(string userInput) {
            IList<MoveCommand> userCommandList = new List<MoveCommand>();
            MoveCommand command;
            for (int i = 0; i<userInput.Length; i++) {
                string userInputKey = userInput[i].ToString();
                command = StaticMoveCommandFactoryDic.commandKeys[userInputKey];
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
               IList<MoveCommand> userCommandList = UserInputToCommands(userInput);
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
