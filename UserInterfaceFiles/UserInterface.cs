﻿using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rover3.MoveCommands.DriveCommandsNS;
using Rover3.MoveCommands.FaceCommandsNS;
using Rover3.MoveCommands.TurnCommandsNS;


namespace Rover3 
{  
    class UserInterface
    {
        //how to make it available everywhere, it and userinterface static? 
        //Do i need to do the same with the class

        //Currently do not use the inteface keys in a way that wouldnt work as a list
        //duplicating keys as set of ifs
        //Change interfaceKey to interface action and make a command pattern dictionary
        //instead of reflection try using classes within the dictionary class rather than in its namespace
        //qqqq
        public struct InterfaceKey : IKeyboardKey
        {
            public string Key { get; set; }

            public string KeyFunctionDescription { get; set; }
            public InterfaceKey(string Key, string KeyFunctionDescription)
            {
                this.Key = Key;
                this.KeyFunctionDescription = KeyFunctionDescription;
            }
        }

        //why not a list ... because can use contains for value
        public IDictionary<string, InterfaceKey> interfaceDic = new Dictionary<string, InterfaceKey>()
        {
            //Destroy rover should be available when moving put with scanning science commands
            { "Q", new InterfaceKey("Q","Press Q to Quit.") },
            { "C", new InterfaceKey("C","Press C to create a new rover.")},
            { "U", new InterfaceKey("U","Press U to get an update on rover positions and status.")},
            { "K", new InterfaceKey("K","Press K for keys and intructions.")}
        };

        //destroy rovers are replying with a report or location etc - does that match what destroy would look like?
        string commandNotRecognisedString = "The following commands were not recognised: ";
        string noLocationChange = "The rover has not been moved please input a valid command string.";
        string validStringRequest = "Please input a valid command string.";
        string userInterfaceCommandKeys = " Q for quit, D for destroy rover, R or return for report on all rovers ";
        string errorUnableToExecuteCommands = "The command sequence is invalid. It could not be exectued at : "; 


        //this should just be a getter
    

        StringBuilder instructions = new StringBuilder(300); //if this is here can reuse it instead of regenerating but is this better
        String partUserCommandsInput = "Interface instructions : Single characters are used to activate user interface commands such as: Press Q to quit, ";
        String partRoverCommandsExample = "Input return with no characters to get a report on the cuurently selected rover.To give rovers commands input the rover you wish to command followed by the command letters you wish it to execute. These commands can be several characters. For example to move rover T forward, rover Y backward and Face rover U east type TFYBUE.If rover T is the currently selected rover you can type FYBUE and the initial commands will be given to rover 'T'.";
        String instructionCreateRover = "There are currently no rovers press C to create one ";
        private string Instructions
        {
            get
            {
                //C create, D destroy,
                instructions.Clear();
                instructions.AppendLine();
                instructions.AppendFormat("{0}{1}{2}", 
                        partUserCommandsInput,
                            "-This will be generated by the usercommands dictionary--",
                            partRoverCommandsExample);
                instructions.AppendLine(RoverCommandsInstructions);
                instructions.AppendLine(RoverKeysInstructions); 
                

                return this.instructions.ToString();
            }
        }

        StringBuilder roverKeyInstructionSB = new StringBuilder(150);
        private string RoverKeysInstructions 
        {
            get 
            {
                roverKeyInstructionSB.Clear();
                instructions.AppendLine();
                roverKeyInstructionSB.AppendLine();
                //(RoverManagerStatic.RoverDictionary.Count > 0 ?  : )   RoversNameKeys string.Join(" ", RoverManagerStatic.RoverDictionary.Keys.ToArray())
                if (RoverManagerStatic.RoverDictionary.Count > 0)
                {
                    
                    roverKeyInstructionSB.AppendLine("Rover(s) :");
                    //for loop faster than foreach but bad with dictionaries
                    //RoverManagerStatic.RoverDictionary.ElementAt(i).Key
                    //for (int i = 0; i < RoverManagerStatic.RoverDictionary.Count; i++)
                    foreach (KeyValuePair<string,Rover> dicEntry in RoverManagerStatic.RoverDictionary)
                    {
                        roverKeyInstructionSB.AppendFormat("        {0} : {1}", dicEntry.Key, dicEntry.Value.KeyFunctionDescription);
                        roverKeyInstructionSB.AppendLine();
                    }
                    return roverKeyInstructionSB.ToString();
                }
                else
                {
                    return roverKeyInstructionSB.AppendLine( instructionCreateRover).ToString(); 
                }
                   
            }
        }

        StringBuilder roverCommandsInstructionSB = new StringBuilder(150);
        private string RoverCommandsInstructions
        {
            get
            {

                roverCommandsInstructionSB.Clear();
                roverCommandsInstructionSB.AppendLine();
                roverCommandsInstructionSB.AppendLine("Rover Drive Commands :"); //Get from dictionary name

                //if i can make a collection of dics will not need to directly access them with different for foreaches
                   

                 

                    foreach (KeyValuePair<string, MoveCommand> driveDicEntry in DriveCommandsDicCS.DriveCommandsDic)
                    {
                        roverCommandsInstructionSB.AppendFormat("        {0} : {1}", driveDicEntry.Key, driveDicEntry.Value.KeyFunctionDescription);
                        roverCommandsInstructionSB.AppendLine();
                    }

               
                    roverCommandsInstructionSB.AppendLine();
                    roverCommandsInstructionSB.AppendLine("Rover Face Commands :"); //Get from dictionary name

                    foreach (KeyValuePair<string, MoveCommand> faceDicEntry in FaceCommandsDicCS.FaceCommandsDic)
                    {
                        roverCommandsInstructionSB.AppendFormat("        {0} : {1}", faceDicEntry.Key, faceDicEntry.Value.KeyFunctionDescription);
                        roverCommandsInstructionSB.AppendLine();
                    }


                    roverCommandsInstructionSB.AppendLine();
                    roverCommandsInstructionSB.AppendLine("Rover Turn Commands :"); //Get from dictionary name

                    foreach (KeyValuePair<string, MoveCommand> turnDicEntry in TurnCommandsDicCS.TurnCommandsDic)
                    {
                        roverCommandsInstructionSB.AppendFormat("        {0} : {1}", turnDicEntry.Key, turnDicEntry.Value.KeyFunctionDescription);
                        roverCommandsInstructionSB.AppendLine();
                    }

                return roverCommandsInstructionSB.ToString();
                
               

            }
        }

        private string _initialInstructions = "Welcome to the rover console program. In this program you can create and move multiple rovers by inputting commands. First create a rover by pressing C and follow the new rover setup instructions. Then create more rovers or give the rover(s) commands. " ;
        private string InitialMessage
        {
            get
            {
                return this._initialInstructions + Instructions;
            }
        }
        //qqqq update this
        private string _keysInUseString;
        private string KeysInUseString
        {
            get
            {
                _keysInUseString = Instructions + userInterfaceCommandKeys;
                return this.KeysInUseString;  
            }
        }


        public UserInterface() {


            string userInput = "";
            ConsoleHandler.DisplayText(InitialMessage);


            while (true) {
                userInput = ConsoleHandler.GetUserInput();
                //if (interfaceDic.ContainsKey(userInput)) { } //how is this 
                if (userInput == "Q")
                {
                    Environment.Exit(0);//could just make it while(loop) snd q makes loop false }

                    if (userInput == "C") { CreateNewRover(); continue; } //else { ConsoleHandler.DisplayText(userInput + " Is not a valid command press C to create a rover or Q to quit"); }

                    if (userInput == "U") { ReportLocationAllRovers(); continue; }//we have report and right both are r
                                                                                  //not a interface command so must be a string
                    ConsoleHandler.DisplayText(CheckProcessUserCommandInput(userInput));
                }
            }
   


        }



            private CommandKeyValidation ValidateCommandKeySeq(String fullCommandStr)
            {

            CommandKeyValidation resultOfCommandSequenceValidation = new CommandKeyValidation(); //is this overwritting the object or adding to it
            resultOfCommandSequenceValidation.ErrorText = commandNotRecognisedString; 
            resultOfCommandSequenceValidation.Valid = true;

            for (int i = 0; i < fullCommandStr.Length; i++)
            {
                if (!( MoveCommandDicManager.KeyExistsInAMoveCommandDictionary(fullCommandStr[i].ToString()) || RoverManagerStatic.RoverDictionary.ContainsKey(fullCommandStr[i].ToString())))
                {
                    fullCommandStr = fullCommandStr.Insert(i, "*").Insert(i + 2, "*");
                    resultOfCommandSequenceValidation.ErrorText += fullCommandStr[i+1] + " ";
                    resultOfCommandSequenceValidation.Valid = false;
                    i += 2;
                } 
            }
            //should I be using task validation report - have one reporting class

            
            if (!resultOfCommandSequenceValidation.Valid) { resultOfCommandSequenceValidation.ErrorText += fullCommandStr + noLocationChange + validStringRequest + ReportLocationSingleRover(RoverManagerStatic.SelectedRover.CurrentLocation); }
            
            return resultOfCommandSequenceValidation;
        
        }
      

        //rearrange text order
        private Rover CreateNewRover() {
            //Should i be making each information grab its own method, if so method in a method, or methods along side
            int newRoverXCoord, newRoverYCoord, newRoverXMin, newRoverXMax, newRoverYMin, newRoverYMax;

            //Orientation newRoverOrientation; // this does work because have no was of setting an orientation just of applying one 
            //to a current locationInfor
            //therefore will need to make a location Information and then apply the command face east to overide our default
            //this may be bad design so maybe I should be setting all orientations based on degrees or radians starting north

            //We have to initialise a location with default values and overwrite it because we can over write an orientation but we have now way of just making one


            LocationInfo newRoverStartLocation= new LocationInfo(new South(),0,0,0,0,0,0,"Q"); 
            //need start default values because need to overwrite orientation cant just select and give it one with keys
            //but can only be set in the constructor currently so need to set when set up
            //which means cannot check within bound bool
            //going to make it available

            string newRoverKey;
            string userInput = "";
            string roverCreationInstructiongString = "During rover creation you cannot use the q, moveCommand keys or create destroy select keys. The newly created rover will be set as the currently selected rover. To set up the rover you will be asked for a single unique key name for it, its current coordinates and its maximum allowed X and Y position as well as it's starting orientation";
            //What if they want to quit during setup
            //Validation

            //Chose a unique single key to assign to your rover

            ConsoleHandler.DisplayText(roverCreationInstructiongString);
           
            bool validInput, validXY, validXYRange;
            do
            {
                ConsoleHandler.DisplayText("Enter a single unique key to select your rover with.");
                validInput = !MoveCommandDicManager.KeyExistsInAMoveCommandDictionary((newRoverKey = ConsoleHandler.GetUserInput())) || newRoverKey == "Q" || newRoverKey == "S" || newRoverKey == "C" || newRoverKey == "D";

                if (!validInput) { ConsoleHandler.DisplayText(newRoverKey + " : " + " is not a unique key"); }
                else { 
                    ConsoleHandler.DisplayText("When the new rover is setup it will be called " + newRoverKey + " when you press the " + newRoverKey + " it will be selected ");
                    newRoverStartLocation.locationFor = newRoverKey;

                }
            }
            while (!validInput);
            
            //Chose your boundaries

            ConsoleHandler.DisplayText("You will now choose the area your rover is allowed to move within by setting a min and max for X and Y coordinates. These numbers are inclusive.");
            //If get boundaries mixed up need a key to allow them to reset that section 

            //Check range has values in it where rovers can be placed
            do
            {
 
                //Choose your Xmin
                do
                {
                    ConsoleHandler.DisplayText("Please choose the minimum X position you want your rover to be able to go to");

                    userInput = ConsoleHandler.GetUserInput();
                    validInput = Int32.TryParse(userInput, out newRoverXMin); //should i be able to drop the  field and use xLowBound
                    if (!validInput) { ConsoleHandler.DisplayText(userInput + " is not a valid integer "); }
                    else
                    {
                        newRoverStartLocation.xLowBound = newRoverXMin;
                        ConsoleHandler.DisplayText("X minimum boundary has been set to " + newRoverStartLocation.xLowBound);
                    }
                }
                while (!validInput);


                //Choose your  YMin

                do
                {
                    ConsoleHandler.DisplayText("Please choose the minimum Y position you want your rover to be able to go to");

                    userInput = ConsoleHandler.GetUserInput();
                    validInput = Int32.TryParse(userInput, out newRoverYMin);
                    if (!validInput) { ConsoleHandler.DisplayText(userInput + " is not a valid integer "); }
                    else
                    {
                        newRoverStartLocation.yLowBound = newRoverYMin;
                        ConsoleHandler.DisplayText("Y minimum boundary has been set to " + newRoverStartLocation.yLowBound);
                    }
                }
                while (!validInput);

                //Choose your  XMax

                do
                {
                    ConsoleHandler.DisplayText("Please choose the maximum X position you want your rover to be able to go to");

                    userInput = ConsoleHandler.GetUserInput();
                    validInput = Int32.TryParse(userInput, out newRoverXMax);
                    if (!validInput)
                    {
                        ConsoleHandler.DisplayText(userInput + " is not a valid integer ");

                    }
                    else if (!(newRoverXMax >= newRoverXMin))
                    {
                        validInput = false;
                        ConsoleHandler.DisplayText(userInput + " the maximum X boundary must be greater than or equal to the minimum. The min boundary is " + newRoverXMin.ToString()); // here will need option to reset min
                    }

                    else
                    {
                        newRoverStartLocation.xHighBound = newRoverXMax;
                        ConsoleHandler.DisplayText("X maximum boundary has been set to " + newRoverStartLocation.xHighBound);
                    }
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
                    else
                    {

                        newRoverStartLocation.yHighBound = newRoverYMax;
                        ConsoleHandler.DisplayText("Y maximum boundary has been set to " + newRoverStartLocation.yHighBound);
                    }
                }
                while (!validInput);

                //check if ranges have any placeable locations
                validXYRange = false;
                bool thisCoordHasRover = false;

                for (int rangeXCoord = newRoverXMin; rangeXCoord <= newRoverXMax; rangeXCoord++) {
                    for (int rangeYCoord = newRoverYMin; rangeYCoord <= newRoverYMax; rangeYCoord++) {
                        thisCoordHasRover = false;
                        foreach (Rover rover in RoverManagerStatic.RoverDictionary.Values)
                        {
                        

                            //continue if space taken
                            if ((rangeXCoord == rover.CurrentLocation.XCoord) && (rangeYCoord == rover.CurrentLocation.YCoord))
                            {
                                thisCoordHasRover = true;
                                break;      //check next coord this one occupied           
                                
                            }
                           
                        }
                        if (thisCoordHasRover) { continue; } else { break; }
                       

                    }
                    if (thisCoordHasRover) { continue; } else { break; }

                }
                validXYRange = !thisCoordHasRover;
                if (!validXYRange) 
                {
                    ConsoleHandler.DisplayText(String.Format("The range ({0}-{1},{2}-{3}) has no available locations within it. Please select a range which includes locations unoccupied by rovers and able to receive your rover. Showing occupied locations : ", newRoverXMin.ToString(), newRoverXMax.ToString(), newRoverYMin.ToString(), newRoverYMax.ToString()));
                    ReportLocationAllRovers();
                }

            } while (!validXYRange);

            //Chose your coords

            ConsoleHandler.DisplayText("You will now choose your starting coords for your new rover. " +
                "They must be within or equal to the range values you set for X and Y. " +
                " For X that is " + newRoverStartLocation.xLowBound.ToString() + "-" + newRoverStartLocation.xHighBound.ToString() +
                " For Y that is " + newRoverStartLocation.yLowBound.ToString() + " - " + newRoverStartLocation.yHighBound.ToString());

            //Chose your XCoord
            do
            {
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
                            ConsoleHandler.DisplayText("X coord has been set to " + newRoverStartLocation.XCoord.ToString());
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
                //need to loop back to the x 
                //need to check if the range user input has any valid locations if not cant make rover with those limits.
                //check if rover already exists in this position
                //we do this in the validation to should this be a rover rover manager method - we should not be operating on the dictionary it should be
                validXY = true;
                foreach (Rover rover in RoverManagerStatic.RoverDictionary.Values)
                {

                    if ((newRoverXCoord == rover.CurrentLocation.XCoord) && (newRoverYCoord == rover.CurrentLocation.YCoord))
                    {
                        //Rover would be created on another rover  
                        ConsoleHandler.DisplayText(String.Format("The given coordinate ({0},{1}) is already occupied by rover {2}. Please chose a different location. Currently occupied locations : ",
                        newRoverStartLocation.XCoord.ToString(), newRoverStartLocation.YCoord.ToString(), rover.RoverKeyName));
                        ReportLocationAllRovers();
                        validXY = false;
                        validInput = false;
                    }
                }
            } while (!validXY);





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
                        newRoverStartLocation = MoveCommandDicManager.SelectCommandFromMoveCommandDics(userInput).ExecuteCommand(newRoverStartLocation);// StaticMoveCommandFactoryDic.commandKeys[userInput].ExecuteCommand(newRoverStartLocation); //do i need to return it to reset it

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
        private string CheckProcessUserCommandInput(string userInput) {

            
            //if the input string is "" could be because they pressed return to get the selected rover report
            //or two names in a row like aza
            //it should say the rover reports string
            //it should check all validations for all the moves if no mves says report
            //use line prepend
            //qqqq
            string successfulCommandExectutionTxt = " The rover(s) successfully executed commands.";
            string justReportingLocations = "Rover reports : ";
            CommandKeyValidation commandKeyValidation = new CommandKeyValidation();
            commandKeyValidation = ValidateCommandKeySeq(userInput);
            if (!commandKeyValidation.Valid) { return commandKeyValidation.ErrorText; }


            IList<RoverTasksValidation> roversTasksValidation = RoverManagerStatic.ValidateCommandStringRouteAndRun(userInput);




            //rover manager
            //IList<MoveCommand> userCommandList = StaticMoveCommandFactoryDic.MoveCommandStrToCmdList(userInput);
            //   roversTasksValidation = RoverManagerStatic.SelectedRover.validateRouteOfCommandSequence(userCommandList);
            
            if (roversTasksValidation[roversTasksValidation.Count-1].CommandsExecutionSuccess)
                {
                //later use lambda expression
                //instead of having to check dictionary and pass the rover should task validation be able to provide info for report
                //Or contain the rover the have validated
                
                bool aRoverMoved = roversTasksValidation.Any(validation => validation.RoverMoved == true);
                StringBuilder individualRoverReportsSB = new StringBuilder(((aRoverMoved) ? successfulCommandExectutionTxt : justReportingLocations), 300);
                for (int i = 0; i < roversTasksValidation.Count; i++) 
                {

                    // individualRoverReportsSB.Append(ReportLocationSingleRover(RoverManagerStatic.RoverDictionary[roversTasksValidation[i].NameOfRover]));
                    individualRoverReportsSB.Append(ReportLocationSingleRover(roversTasksValidation[i].TaskEndLocation)); //TaskEndLocation
                }

                return individualRoverReportsSB.ToString(); 
                
                }
                else {
                //first this needs changing so validates key and the validate is a seperate method
                // then need to extract method for validating the task
                //then validations once set up right need altering to be based on dictionaries

                StringBuilder errorWithTaskValidation = new StringBuilder(150);
                errorWithTaskValidation.AppendLine(); //so not connected to anything added to 
                errorWithTaskValidation.AppendLine(errorUnableToExecuteCommands);
                errorWithTaskValidation.AppendLine(userInput.Insert(roversTasksValidation[roversTasksValidation.Count - 1].InvalidCommandIndex, "*").Insert(roversTasksValidation[roversTasksValidation.Count - 1].InvalidCommandIndex + 2, "*"));
            

                //!!!!!! Should say which rover
                errorWithTaskValidation.AppendFormat(
                    "because it would {0} at X = {1} and Y = {2}.",
                    ((roversTasksValidation[roversTasksValidation.Count - 1].NameOfRoverCollidedWith == null)? "be out of bounds": ("collide with rover " + roversTasksValidation[roversTasksValidation.Count - 1].NameOfRoverCollidedWith)), 
                    roversTasksValidation[roversTasksValidation.Count - 1].WhereCommandBecomesInvalid.XCoord.ToString(),
                    roversTasksValidation[roversTasksValidation.Count - 1].WhereCommandBecomesInvalid.YCoord.ToString()
                    );
                
                errorWithTaskValidation.AppendLine(noLocationChange);
                errorWithTaskValidation.AppendLine(validStringRequest);
                errorWithTaskValidation.AppendLine();
                //should it tell you the rovers bounds - could do this in the location report
                return errorWithTaskValidation.ToString(); 
                }
        }
        //uses current location not test so if called during route validation will fail
        //need to rename test location so it is assigned location or something so it is the one to check
        private void ReportLocationAllRovers() {
            int numberOfRovers = RoverManagerStatic.RoverDictionary.Count;
            if (numberOfRovers == 0) { ConsoleHandler.DisplayText(instructionCreateRover); } else { 
            StringBuilder allRoversReport = new StringBuilder(150* numberOfRovers);
            foreach (Rover rover in RoverManagerStatic.RoverDictionary.Values)
            {
                allRoversReport.Append(ReportLocationSingleRover(rover.CurrentLocation));

            }
            ConsoleHandler.DisplayText(allRoversReport.ToString());
            }
        }

        //currently repeating this function - if location information 
        private string ReportLocationSingleRover(LocationInfo locationInfo) { //this should be roverTaskValidation it is doing too much, it should be the rover that has its location history 
//this is always called using a rover.location so could be in the rover and reporting its own location
//however try to keep all string building in user interface
//would also mean do not have to assign a location what it is location for as just ask the rover its name not location what is assigned to it
            StringBuilder LocationReport = new StringBuilder(150);
            //Append Line so not on same line as what being joined to
            LocationReport.AppendLine();
            LocationReport.AppendFormat("ROVER {0}  REPORT: {1}Selected Rover Name {0}", locationInfo.locationFor, Environment.NewLine);
            LocationReport.AppendFormat("{0}Rover location is X: {1} Y:{2}", Environment.NewLine, locationInfo.XCoord.ToString(), locationInfo.YCoord.ToString());
            LocationReport.AppendFormat("{0}Rover is facing {1}", Environment.NewLine, locationInfo.myOrientation.orientationName);
            LocationReport.AppendLine();
            //Append Line so not on same line as what being joined to and if they do the same there should be a gap space

            return LocationReport.ToString();
        }
       

    }
}

