using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover3 
{
    class UserInterface
    {
        //put line breaks 
        //ConsoleHandler ConsoleHandler = new ConsoleHandler(); //only ever 1 make static?


        //Later should request start locmation of rover 
        // private Orientation StartSouth = new South();
        //private int   initXCoord=0, initYCoord=0, xLowBound = 0, xHighBound = 10, yLowBound = 0, yHighBound = 10;

        //LocationInfo roverInitLocation = new LocationInfo(StartSouth, initXCoord, initYCoord, xLowBound, xHighBound, yLowBound, yHighBound );



        //Rover selectedRover;// = new Rover(new LocationInfo(new North(), 0, 0, 0, 10, 0, 10));
        //maybe dictionary should hold this and getter and setter should alter the selectedRover
        //or being an object will changes translate anywya
        //maybe should store test rover here too 
        //and make dictionary rover manager

        string errorNoLocationChange = " The rover has not been moved please input a valid command string.";
        //should i have lots shared stored strings?

        //later make this a key press to add a rover to the a list of rovers accessible with a number
        public UserInterface() {

            //do introduction stuff
            //while true
            //quit the loop by quit function in dictionary 
            


            string userInput = "";
            ConsoleHandler.DisplayText(InitialMessage());


            while (true) {
                userInput = ConsoleHandler.GetUserInput();
                if (userInput == "Q") { Environment.Exit(0); }
                      
                if (userInput == "C") {  CreateNewRover(); break; } else { ConsoleHandler.DisplayText(userInput + " Is not a valid command press C to create a rover or Q to quit"); }
            }
            // later remove and it will be an instuction
            ConsoleHandler.DisplayText(ReportLocation());
            ConsoleHandler.DisplayText(Instructions());
            
            
            while ((userInput = ConsoleHandler.GetUserInput()) != "Q")
            {
                
                //if no rover set ask for rover if there are some, or to create one if not
                //Select Rover or Create rover
                //If S select rover
                //If C create rover + set as selected rover
                //IF D destroy current rover 
                //Input Command for that Rover
                string commandInput = userInput; // later different inputs depending which dic of commands
                ConsoleHandler.DisplayText(CheckProcessUserCommandInput(commandInput));
            }
       

            //need string input to IList
            // rover.runCommandSequence(ConsoleHandler.getUserInput());


        }
        public string InitialMessage() {
            string initialInstructions = "First create a rover by pressing C. Then move the rover with movement commands " +" (later keys and decriptions generated here) "+" The user interface commands are " +"(Here have key dictionary for C create rover and D destroy rover and current rovers)";
            return initialInstructions;
        }
        public string Instructions() {
            return "Available commands : " + string.Join(" ", StaticMoveCommandFactoryDic.commandKeys.Keys.ToArray()) + " Rovers " + string.Join(" ", RoverManagerStatic.RoverDictionary.Keys.ToArray());
  
        }
        public CommandKeyValidation ValidateCommandKeySeq(String commandString) {

            
            CommandKeyValidation resultOfCommandSequenceValidation = new CommandKeyValidation();
            resultOfCommandSequenceValidation.errorText += "The following commands were not recognised: ";
            resultOfCommandSequenceValidation.valid = true;



            for (int i = 0; i < commandString.Length; i++)
            {

                if (!( StaticMoveCommandFactoryDic.commandKeys.ContainsKey(commandString[i].ToString()) || RoverManagerStatic.RoverDictionary.ContainsKey(commandString[i].ToString())))
                {
                    commandString = commandString.Insert(i, "*").Insert(i + 2, "*");
                    resultOfCommandSequenceValidation.errorText += commandString[i+1] + " ";
                    resultOfCommandSequenceValidation.valid = false;
                    i += 2;

                }

                               
            }

        

            
            if (resultOfCommandSequenceValidation.valid) { resultOfCommandSequenceValidation.errorText = ""; } else { resultOfCommandSequenceValidation.errorText += commandString+ errorNoLocationChange + ReportLocation(); ; }
            return resultOfCommandSequenceValidation;
        
        }
      

        //rearrange text order
        public Rover CreateNewRover() {
            //Should i be making each information grab its own method, if so method in a method, or methods along side
            int newRoverXCoord, newRoverYCoord, newRoverXMin, newRoverXMax, newRoverYMin, newRoverYMax;

            //Orientation newRoverOrientation; // this does work because have no was of setting an orientation just of applying one 
            //to a current locationInfor
            //therefore will need to make a location Information and then apply the command face east to overide our default
            //this may be bad design so maybe I should be setting all orientations based on degrees or radians starting north

            //We have to initialise a location with default values and overwrite it because we can over write an orientation but we have now way of just making one


            LocationInfo newRoverStartLocation= new LocationInfo(new South(),0,0,0,0,0,0); 
            //need start default values because need to overwrite orientation cant just select and give it one with keys
            //but can only be set in the constructor currently so need to set when set up
            //which means cannot check within bound bool
            //going to make it available

            string newRoverKey;
            string userInput = "";

            //What if they want to quit during setup
            //Validation

            //Chose a unique single key to assign to your rover

            ConsoleHandler.DisplayText("During rover creation you cannot use the q, moveCommand keys or create destroy select keys. The newly created rover will be set as the currently selected rover. To set up the rover you will be asked for a single unique key name for it, its current coordinates and its maximum allowed X and Y position as well as it's starting orientation");
            string keysAlreadyInUse = Instructions() + " Q S C D "; // later needs to be moveCommand + interfaceCommands
            bool validInput;
            do
            {
                ConsoleHandler.DisplayText("Enter a single unique key to select your rover with.");
                validInput = !StaticMoveCommandFactoryDic.commandKeys.ContainsKey((newRoverKey = ConsoleHandler.GetUserInput())) || newRoverKey == "Q" || newRoverKey == "S" || newRoverKey == "C" || newRoverKey == "D";

                if (!validInput) { ConsoleHandler.DisplayText(newRoverKey + " : " + " is not a unique key"); }
                else { ConsoleHandler.DisplayText("When the new rover is setup it will be called " + newRoverKey + " when you press the " + newRoverKey + " it will be selected "); }
            }
            while (!validInput);
            
            //Chose your boundaries

            ConsoleHandler.DisplayText("You will now choose the area your rover is allowed to move within by setting a min and max for X and Y coordinates. These numbers are inclusive.");
            //If get boundaries mixed up need a key to allow them to reset that section 


            //Choose your Xmin
            do
            {
                ConsoleHandler.DisplayText("Please choose the minimum X position you want your rover to be able to go to");

                userInput = ConsoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverXMin); //should i be able to drop the  field and use xLowBound
                if (!validInput) { ConsoleHandler.DisplayText(userInput + " is not a valid integer "); }
                else {
                 newRoverStartLocation.xLowBound = newRoverXMin;
                    ConsoleHandler.DisplayText("X minimum boundary has been set to " + newRoverStartLocation.xLowBound); }
            }
            while (!validInput);


            //Choose your  YMin

            do
            {
                ConsoleHandler.DisplayText("Please choose the minimum Y position you want your rover to be able to go to");

                userInput = ConsoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverYMin);
                if (!validInput) { ConsoleHandler.DisplayText(userInput + " is not a valid integer "); }
                else {
                   newRoverStartLocation.yLowBound = newRoverYMin;
                    ConsoleHandler.DisplayText("Y minimum boundary has been set to " + newRoverStartLocation.yLowBound ); }
            }
            while (!validInput);

            //Choose your  XMax

            do
            {
                ConsoleHandler.DisplayText("Please choose the maximum X position you want your rover to be able to go to");

                userInput = ConsoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverXMax);
                if (!validInput) { ConsoleHandler.DisplayText(userInput + " is not a valid integer ");

                } else if(!(newRoverXMax >= newRoverXMin))
                    {
                    validInput = false;
                    ConsoleHandler.DisplayText(userInput + " the maximum X boundary must be greater than or equal to the minimum. The min boundary is " + newRoverXMin.ToString()); // here will need option to reset min
                }
                
                else {
                    newRoverStartLocation.xHighBound = newRoverXMax;
                    ConsoleHandler.DisplayText("X maximum boundary has been set to " + newRoverStartLocation.xHighBound ); }
            }
            while (!validInput);

            //Choose your  YMax

            do
            {
                ConsoleHandler.DisplayText("Please choose the maximum Y position you want your rover to be able to go to");

                userInput = ConsoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverYMax);
                if (!validInput)
                {
                    ConsoleHandler.DisplayText(userInput + " is not a valid integer ");

                }
                else if (!(newRoverYMax >= newRoverYMin))
                {
                    validInput = false;
                    ConsoleHandler.DisplayText(userInput + " the maximum X boundary must be greater than or equal to the minimum. The min boundary is " + newRoverXMin.ToString()); // here will need option to reset min
                }
                else{

               newRoverStartLocation.yHighBound = newRoverYMax;
                ConsoleHandler.DisplayText("Y maximum boundary has been set to " + newRoverStartLocation.yHighBound);
                }
            }
            while (!validInput);


            //Chose your coords

            ConsoleHandler.DisplayText("You will now choose your starting coords for your new rover. " +
                "They must be within or equal to the range values you set for X and Y. " +
                " For X that is " + newRoverStartLocation.xLowBound.ToString()+"-"+ newRoverStartLocation.xHighBound.ToString()+ 
                " For Y that is " + newRoverStartLocation.yLowBound.ToString() +" - "+ newRoverStartLocation.yHighBound.ToString());

            //Chose your XCoord

            do
            {
                ConsoleHandler.DisplayText("Please choose an initial X coordinate value for the rover.");
                
                userInput = ConsoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverXCoord);
                if (!validInput) { ConsoleHandler.DisplayText(userInput + " is not a valid integer "); }
                else
                {
                    newRoverStartLocation.XCoord = newRoverXCoord;
                    if (!newRoverStartLocation.withinXBounds)
                    {
                        validInput = false;
                        ConsoleHandler.DisplayText("The given X coord " + newRoverStartLocation.XCoord.ToString() + " is not within the X bounds of " + newRoverStartLocation.xLowBound.ToString() + "-" + newRoverStartLocation.xHighBound.ToString());
                    }
                    else
                    {
                        ConsoleHandler.DisplayText("X coord has been set to " + newRoverStartLocation.XCoord.ToString() );
                    }
                }
            }
            
            while (!validInput);


                //Chose your YCoord

            do
            {
                ConsoleHandler.DisplayText("Please choose an initial Y coordinate value for the rover");
                userInput = ConsoleHandler.GetUserInput();
                validInput = Int32.TryParse(userInput, out newRoverYCoord);
                    if (!validInput) { ConsoleHandler.DisplayText(userInput + " is not a valid integer "); }
                    else
                    {
                        newRoverStartLocation.YCoord = newRoverYCoord;
                        if (!newRoverStartLocation.withinYBounds)
                        {
                        validInput = false;
                        ConsoleHandler.DisplayText("The given Y coord " + newRoverStartLocation.YCoord.ToString() + " is not within the Y bounds of " + newRoverStartLocation.yLowBound.ToString() + "-" + newRoverStartLocation.yHighBound.ToString());
                        }
                        else
                        {
                            ConsoleHandler.DisplayText("Y coord has been set to " + newRoverStartLocation.YCoord.ToString());
                        }
                    }
                    
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

            ConsoleHandler.DisplayText("Please choose an initial starting orientation for your rover");

            do
            {
                ConsoleHandler.DisplayText("Enter a starting orientation for your rover it must be one of N E S W."); // later should come from the orientation dictionary 
                userInput = ConsoleHandler.GetUserInput();
                validInput = (userInput == "N"||userInput=="E"||userInput=="S"||userInput=="W"); 

                if (!validInput) { ConsoleHandler.DisplayText(userInput + " : " + " is not a valid orientation"); }
                else {
                        newRoverStartLocation = StaticMoveCommandFactoryDic.commandKeys[userInput].ExecuteCommand(newRoverStartLocation); //do i need to return it to reset it

                    ConsoleHandler.DisplayText("The new orientation is " + newRoverStartLocation.myOrientation.orientationName); 
                }
            }
            while (!validInput);



            //dictionary should create and return the rover the userInterface should just get the info

            Rover newRover = new Rover(newRoverKey, newRoverStartLocation); // later it will need to get the new key too
            RoverManagerStatic.AddRoverToRoverDictionary(newRover);
            RoverManagerStatic.SelectedRover = RoverManagerStatic.RoverDictionary[newRoverKey];

            ConsoleHandler.DisplayText("Rover created");
            return newRover;

        }
       
       // public string ChangeRover(Rover roverWantSelected) { selectedRover = roverWantSelected }
        //seperate out string validation the command be executable is for rover manager to decide
        public string CheckProcessUserCommandInput(string userInput) {
            
            //first this needs changing so validates key and the validate is a seperate method
            // then need to extract method for validating the task
            //then validations once set up right need altering to be based on dictionaries
           

            string successfulCommandExectutionTxt = " The rover has successfully been moved.";

            CommandKeyValidation commandKeyValidation = new CommandKeyValidation();
            commandKeyValidation = ValidateCommandKeySeq(userInput);
            if (!commandKeyValidation.valid) { return commandKeyValidation.errorText; }

            
            RoversTasksValidation roversTasksValidation = new RoversTasksValidation();
            
          

            
            
               IList<MoveCommand> userCommandList = UserInputToCommands(userInput);
                roversTasksValidation = RoverManagerStatic.SelectedRover.validateRouteOfCommandSequence(userCommandList);
                if (roversTasksValidation.CommandsExecutionSuccess)
                {
                    return successfulCommandExectutionTxt + ReportLocation();

                }
                else {
                    //differentiating from out of bound or object or rover would be good
                    // telling them the bounds would be good to 
                    string errorUnableToExecuteCommands = "The command sequence is invalid. It could not be exectued at : ";
                    errorUnableToExecuteCommands += userInput.Insert(roversTasksValidation.InvalidCommandIndex, "*").Insert(roversTasksValidation.InvalidCommandIndex + 2, "*");
                    errorUnableToExecuteCommands +=  string.Format(" because it would be out of bounds at X = {0} and Y = {1}.", roversTasksValidation.WhereCommandBecomesInvalid.XCoord.ToString(), roversTasksValidation.WhereCommandBecomesInvalid.YCoord.ToString());
                    errorUnableToExecuteCommands += errorNoLocationChange;
                    return errorUnableToExecuteCommands;


                }

            
        }

        public string ReportLocation() {

            string LocationReport = "Selected rover name: ";
            LocationReport += RoverManagerStatic.SelectedRover.RoverKeyName + " ";
            LocationReport += "rover location: ";
            LocationReport += "x location is ";
            LocationReport += RoverManagerStatic.SelectedRover.currentLocation.XCoord.ToString() + ". ";
            LocationReport += "y location is ";
            LocationReport += RoverManagerStatic.SelectedRover.currentLocation.YCoord.ToString() + ". ";
            LocationReport += "rover is facing ";
            LocationReport += RoverManagerStatic.SelectedRover.currentLocation.myOrientation.orientationName + ". ";
            return LocationReport;
        }
        

    }
}
