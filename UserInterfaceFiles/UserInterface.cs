using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rover3.MoveCommands.DriveCommandsNS;
using Rover3.MoveCommands.FaceCommandsNS;
using Rover3.MoveCommands.TurnCommandsNS;
using Rover3;

namespace Rover3 

{

    public abstract class InterfaceKey : ConsoleHandler, IKeyboardKey

    {
        //uses current location not test so if called during route validation will fail
        //need to rename test location so it is assigned location or something so it is the one to check
        public void ReportLocationAllRovers()
        {
            int numberOfRovers = RoverManagerStatic.RoverDictionary.Count;
            if (numberOfRovers == 0) { DisplayText(instructionCreateRover); }
            else
            {
                StringBuilder allRoversReport = new StringBuilder(150 * numberOfRovers);
                foreach (Rover rover in RoverManagerStatic.RoverDictionary.Values)
                {
                    allRoversReport.Append(ReportLocationSingleRover(rover.CurrentLocation));

                }
                DisplayText(allRoversReport.ToString());
            }
        }

        public String instructionCreateRover = "There are currently no rovers press C to create one ";
        //currently repeating this function - if location information 
        
        public abstract string Key { get; }

        public abstract string KeyFunctionDescription { get; }

        public abstract void KeyAction();

    }

    public class U : InterfaceKey

    {
        public override string Key { get { return "U"; } }

        public override string KeyFunctionDescription { get { return "Press U to get an update on rover positions and status."; } }

        public override void KeyAction()
        {
            ReportLocationAllRovers();

        }
    }

    public class Q : InterfaceKey

    {
        public override string Key { get { return "Q"; } }

        public override string KeyFunctionDescription { get { return "Press Q to quit."; } }

        public override void KeyAction()
        {
            Environment.Exit(0);

        }
    }

    public class D : InterfaceKey

    {
        public override string Key { get { return "D"; } }

        public override string KeyFunctionDescription { get { return "Press D to destroy currently selected Rover."; } }

        public override void KeyAction()
        {
            if (RoverManagerStatic.ARoverIsCurrentlySelected)
            {
                String selectedRoverKey = RoverManagerStatic.SelectedRover.Key;

                if (RoverManagerStatic.RemoveSelectedRoverFromDictionary())
                {

                    StringBuilder sb = new StringBuilder("Rover : " + selectedRoverKey + "  has been destroyed.", 150);

                    if (RoverManagerStatic.RoverDictionary.Count < 1)
                    {
                        sb.AppendLine();
                        sb.AppendLine(instructionCreateRover);

                    }
                    DisplayText(sb.ToString());
                }
                else
                {
                    DisplayText("Rover : " + selectedRoverKey + " does not exist so could not be destroyed");

                };

            }
            else { DisplayText("No Rover currently selected, select a rover then return then press d to delete it"); }
        }
    }


    public class C : InterfaceKey

    {
        public override string Key { get { return "C"; } }

        public override string KeyFunctionDescription { get { return "Press C to create a new rover."; } }

        public override void KeyAction()
        {
            
                //Should i be making each information grab its own method, if so method in a method, or methods along side
                int newRoverXCoord, newRoverYCoord, newRoverXMin, newRoverXMax, newRoverYMin, newRoverYMax;

                //Orientation newRoverOrientation; // this does work because have no was of setting an orientation just of applying one 
                //to a current locationInfor
                //therefore will need to make a location Information and then apply the command face east to overide our default
                //this may be bad design so maybe I should be setting all orientations based on degrees or radians starting north

                //We have to initialise a location with default values and overwrite it because we can over write an orientation but we have now way of just making one


                LocationInfo newRoverStartLocation = new LocationInfo(new South(), 0, 0, 0, 0, 0, 0, "Q");
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

                DisplayText(roverCreationInstructiongString);

                bool validInput, validXY, validXYRange;
                do
                {
                    DisplayText("Enter a single unique key to select your rover with.");
                    validInput = !MoveCommandDicManager.KeyExistsInAMoveCommandDictionary((newRoverKey = GetUserInput())) || newRoverKey == "Q" || newRoverKey == "S" || newRoverKey == "C" || newRoverKey == "D";

                    if (!validInput) { DisplayText(newRoverKey + " : " + " is not a unique key"); }
                    else
                    {
                        DisplayText("When the new rover is setup it will be called " + newRoverKey + " when you press the " + newRoverKey + " it will be selected ");
                        newRoverStartLocation.locationFor = newRoverKey;

                    }
                }
                while (!validInput);

                //Chose your boundaries

                DisplayText("You will now choose the area your rover is allowed to move within by setting a min and max for X and Y coordinates. These numbers are inclusive.");
                //If get boundaries mixed up need a key to allow them to reset that section 

                //Check range has values in it where rovers can be placed
                do
                {

                    //Choose your Xmin
                    do
                    {
                        DisplayText("Please choose the minimum X position you want your rover to be able to go to");

                        userInput = GetUserInput();
                        validInput = Int32.TryParse(userInput, out newRoverXMin); //should i be able to drop the  field and use xLowBound
                        if (!validInput) { DisplayText(userInput + " is not a valid integer "); }
                        else
                        {
                            newRoverStartLocation.xLowBound = newRoverXMin;
                            DisplayText("X minimum boundary has been set to " + newRoverStartLocation.xLowBound);
                        }
                    }
                    while (!validInput);


                    //Choose your  YMin

                    do
                    {
                        DisplayText("Please choose the minimum Y position you want your rover to be able to go to");

                        userInput = GetUserInput();
                        validInput = Int32.TryParse(userInput, out newRoverYMin);
                        if (!validInput) { DisplayText(userInput + " is not a valid integer "); }
                        else
                        {
                            newRoverStartLocation.yLowBound = newRoverYMin;
                            DisplayText("Y minimum boundary has been set to " + newRoverStartLocation.yLowBound);
                        }
                    }
                    while (!validInput);

                    //Choose your  XMax

                    do
                    {
                        DisplayText("Please choose the maximum X position you want your rover to be able to go to");

                        userInput = GetUserInput();
                        validInput = Int32.TryParse(userInput, out newRoverXMax);
                        if (!validInput)
                        {
                            DisplayText(userInput + " is not a valid integer ");

                        }
                        else if (!(newRoverXMax >= newRoverXMin))
                        {
                            validInput = false;
                            DisplayText(userInput + " the maximum X boundary must be greater than or equal to the minimum. The min boundary is " + newRoverXMin.ToString()); // here will need option to reset min
                        }

                        else
                        {
                            newRoverStartLocation.xHighBound = newRoverXMax;
                            DisplayText("X maximum boundary has been set to " + newRoverStartLocation.xHighBound);
                        }
                    }
                    while (!validInput);

                    //Choose your  YMax

                    do
                    {
                        DisplayText("Please choose the maximum Y position you want your rover to be able to go to");

                        userInput = GetUserInput();
                        validInput = Int32.TryParse(userInput, out newRoverYMax);
                        if (!validInput)
                        {
                            DisplayText(userInput + " is not a valid integer ");

                        }
                        else if (!(newRoverYMax >= newRoverYMin))
                        {
                            validInput = false;
                            DisplayText(userInput + " the maximum X boundary must be greater than or equal to the minimum. The min boundary is " + newRoverXMin.ToString()); // here will need option to reset min
                        }
                        else
                        {

                            newRoverStartLocation.yHighBound = newRoverYMax;
                            DisplayText("Y maximum boundary has been set to " + newRoverStartLocation.yHighBound);
                        }
                    }
                    while (!validInput);

                    //check if ranges have any placeable locations
                    validXYRange = false;
                    bool thisCoordHasRover = false;

                    for (int rangeXCoord = newRoverXMin; rangeXCoord <= newRoverXMax; rangeXCoord++)
                    {
                        for (int rangeYCoord = newRoverYMin; rangeYCoord <= newRoverYMax; rangeYCoord++)
                        {
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
                        DisplayText(String.Format("The range ({0}-{1},{2}-{3}) has no available locations within it. Please select a range which includes locations unoccupied by rovers and able to receive your rover. Showing occupied locations : ", newRoverXMin.ToString(), newRoverXMax.ToString(), newRoverYMin.ToString(), newRoverYMax.ToString()));
                        ReportLocationAllRovers();
                    }

                } while (!validXYRange);

                //Chose your coords

                DisplayText("You will now choose your starting coords for your new rover. " +
                    "They must be within or equal to the range values you set for X and Y. " +
                    " For X that is " + newRoverStartLocation.xLowBound.ToString() + "-" + newRoverStartLocation.xHighBound.ToString() +
                    " For Y that is " + newRoverStartLocation.yLowBound.ToString() + " - " + newRoverStartLocation.yHighBound.ToString());

                //Chose your XCoord
                do
                {
                    do
                    {
                        DisplayText("Please choose an initial X coordinate value for the rover.");

                        userInput = GetUserInput();
                        validInput = Int32.TryParse(userInput, out newRoverXCoord);
                        if (!validInput) { DisplayText(userInput + " is not a valid integer "); }
                        else
                        {
                            newRoverStartLocation.XCoord = newRoverXCoord;
                            if (!newRoverStartLocation.withinXBounds)
                            {
                                validInput = false;
                                DisplayText("The given X coord " + newRoverStartLocation.XCoord.ToString() + " is not within the X bounds of " + newRoverStartLocation.xLowBound.ToString() + "-" + newRoverStartLocation.xHighBound.ToString());
                            }
                            else
                            {
                                DisplayText("X coord has been set to " + newRoverStartLocation.XCoord.ToString());
                            }
                        }
                    }

                    while (!validInput);


                    //Chose your YCoord

                    do
                    {
                        DisplayText("Please choose an initial Y coordinate value for the rover");
                        userInput = GetUserInput();
                        validInput = Int32.TryParse(userInput, out newRoverYCoord);
                        if (!validInput) { DisplayText(userInput + " is not a valid integer "); }
                        else
                        {
                            newRoverStartLocation.YCoord = newRoverYCoord;
                            if (!newRoverStartLocation.withinYBounds)
                            {
                                validInput = false;
                                DisplayText("The given Y coord " + newRoverStartLocation.YCoord.ToString() + " is not within the Y bounds of " + newRoverStartLocation.yLowBound.ToString() + "-" + newRoverStartLocation.yHighBound.ToString());
                            }
                            else
                            {
                                DisplayText("Y coord has been set to " + newRoverStartLocation.YCoord.ToString());
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
                            DisplayText(String.Format("The given coordinate ({0},{1}) is already occupied by rover {2}. Please chose a different location. Currently occupied locations : ",
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

                DisplayText("Please choose an initial starting orientation for your rover");

                do
                {
                    DisplayText("Enter a starting orientation for your rover it must be one of N E S W."); // later should come from the orientation dictionary 
                    userInput = GetUserInput();
                    validInput = (userInput == "N" || userInput == "E" || userInput == "S" || userInput == "W");

                    if (!validInput) { DisplayText(userInput + " : " + " is not a valid orientation"); }
                    else
                    {
                        newRoverStartLocation = MoveCommandDicManager.SelectCommandFromMoveCommandDics(userInput).ExecuteCommand(newRoverStartLocation);// StaticMoveCommandFactoryDic.commandKeys[userInput].ExecuteCommand(newRoverStartLocation); //do i need to return it to reset it

                        DisplayText("The new orientation is " + newRoverStartLocation.myOrientation.orientationName);
                    }
                }
                while (!validInput);



             

              
                RoverManagerStatic.AddRoverToRoverDictionary(newRoverKey, newRoverStartLocation);
                

                DisplayText("Rover created");
                //return newRover;

            

        }
    }

    public class K : InterfaceKey

    {
        //why not a list ... because can use contains for value
        public IDictionary<string, InterfaceKey> interfaceDic = new Dictionary<string, InterfaceKey>()
        {
            //Destroy rover should be available when moving put with scanning science commands
          
            { new C().Key, new C()},
            //{ new K().Key, new K()}, //will cause stack overflow as is circular reference
            { new U().Key, new U()},
            { new Q().Key, new Q()},
            { new D().Key, new D()}

        };
        public override string Key { get { return "K"; } }

        public override string KeyFunctionDescription { get { return "Press K to get a list of keys and their descriptions."; } }

        public override void KeyAction()
        {
            StringBuilder sb = new StringBuilder(200);
            //how can this be a key for reporting on itself
             sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Interface keys must be entered as a single character");
            foreach (IKeyboardKey dicEntry in interfaceDic.Values) 
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Movement, Orientation, Turn, and Select Rover commands can be entered as a long string");
            sb.AppendLine();
            sb.AppendLine("Available Rovers");
            sb.AppendLine();

            foreach (IKeyboardKey dicEntry in RoverManagerStatic.RoverDictionary.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Drive Commands");
            sb.AppendLine();

            //if (DriveCommandsDicCS.DriveCommandsDic.ContainsKey(userInputKey)) { return DriveCommandsDicCS.DriveCommandsDic[userInputKey]; }
            //if (FaceCommandsDicCS.FaceCommandsDic.ContainsKey(userInputKey)) { return FaceCommandsDicCS.FaceCommandsDic[userInputKey]; }
            //if (TurnCommandsDicCS.TurnCommandsDic.ContainsKey(userInputKey)
            foreach (IKeyboardKey dicEntry in DriveCommandsDicCS.DriveCommandsDic.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine("Face Commands");
            sb.AppendLine();

            foreach (IKeyboardKey dicEntry in FaceCommandsDicCS.FaceCommandsDic.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine("Turns Commands");
            sb.AppendLine();

            foreach (IKeyboardKey dicEntry in TurnCommandsDicCS.TurnCommandsDic.Values)
            {
                sb.AppendFormat("{0} : {1}", dicEntry.Key, dicEntry.KeyFunctionDescription);
                sb.AppendLine();
            }




            DisplayText(sb.ToString());

        }
    }
    class UserInterface  : ConsoleHandler
    {
        //how to make it available everywhere, it and userinterface static? 
        //Do i need to do the same with the class

        //Currently do not use the inteface keys in a way that wouldnt work as a list
        //duplicating keys as set of ifs
        //Change interfaceKey to interface action and make a command pattern dictionary
        //instead of reflection try using classes within the dictionary class rather than in its namespace
        //qqqq


     

        //destroy rovers are replying with a report or location etc - does that match what destroy would look like?
        string commandNotRecognisedString = "The following commands were not recognised: ";
        string noLocationChange = " The rovers have not been moved please input a valid command string."; //this gets shown if an incorrect rover enterred
        string validStringRequest = "Please input a valid command string.";
        string userInterfaceCommandKeys = " Q for quit, D for destroy rover, R or return for report on all rovers ";
        string errorUnableToExecuteCommands = "The command sequence is invalid. It could not be exectued at : "; 


        //this should just be a getter
    

        StringBuilder instructions = new StringBuilder(300); //if this is here can reuse it instead of regenerating but is this better
        String partUserCommandsInput = "Interface instructions : Single characters are used to activate user interface commands such as: Press Q to quit, ";
        String partRoverCommandsExample = "Input return with no characters to get a report on the cuurently selected rover.To give rovers commands input the rover you wish to command followed by the command letters you wish it to execute. These commands can be several characters. For example to move rover T forward, rover Y backward and Face rover U east type TFYBUE.If rover T is the currently selected rover you can type FYBUE and the initial commands will be given to rover 'T'.";
    
       
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
                    return roverKeyInstructionSB.AppendLine( new C().instructionCreateRover).ToString(); 
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
        private string instructionCreateRover;

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
            DisplayText(InitialMessage);

            //dont like this
            K k = new K();
            //qqqqq start
            while (true)
            {

                userInput = GetUserInput();
                //if (interfaceDic.ContainsKey(userInput)) { } //how is this 

                //could just make it while(loop) snd q makes loop false }#//else { ConsoleHandler.DisplayText(userInput + " Is not a valid command press C to create a rover or Q to quit"); }
                //we have report and right both are r
                //not a interface command so must be a string
                if (userInput == k.Key) { k.KeyAction(); }
                else if (k.interfaceDic.ContainsKey(userInput)) { k.interfaceDic[userInput].KeyAction(); continue; }
                else
                {
                    DisplayText(CheckProcessUserCommandInput(userInput));
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

            //repeats self and not make sense
            if (!resultOfCommandSequenceValidation.Valid) { resultOfCommandSequenceValidation.ErrorText += fullCommandStr + noLocationChange + validStringRequest + ReportLocationSingleRover(); }
            
            return resultOfCommandSequenceValidation;
        
        }
      

        //rearrange text order
        
       
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
           
            if ((RoverManagerStatic.ARoverIsCurrentlySelected == false) && ((userInput.Length > 1) || (RoverManagerStatic.RoverDictionary.ContainsKey(userInput[0].ToString())))) { return noRoverSelectedMsg; }

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
        

        

       


    }
}

