﻿using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rover3.MoveCommands.DriveCommandsNS;
using Rover3.MoveCommands.FaceCommandsNS;
using Rover3.MoveCommands.TurnCommandsNS;
using Rover3;
using Rover3.UserInterfaceFiles;

namespace Rover3 

{

    
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
        string noLocationChange = " No rovers have been moved please input a valid command string."; //this gets shown if an incorrect rover enterred
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
               
                if (UserInterfaceDic.interfaceDic.ContainsKey(userInput)) { UserInterfaceDic.interfaceDic[userInput].KeyAction(); continue; }
                else
                {
                    DisplayText(CheckProcessUserCommandInput(userInput));
                }
            }
   


        }

            private string CommandStringAsLettersOnly(String fullCommandStr) 
            {
            
                
                string letterToRepeat = (RoverManagerStatic.ARoverIsCurrentlySelected)? RoverManagerStatic.SelectedRover.Key: "Error due to no selected rover and number first";
                string lettersToAdd;

                for (int i = 0; i < fullCommandStr.Length; i++)
                {
                //Console.WriteLine("string is now : " + fullCommandStr + "  i: " + i.ToString() + "    and in i loop");
                int number;
                    int endOfNumberIndex;
                    int startOfNumberIndex;
                    if (Int32.TryParse(fullCommandStr[i].ToString(), out number))
                    {
                        startOfNumberIndex = i;
                    }
                    else
                    {
                        //if (RoverManagerStatic.RoverDictionary.ContainsKey(fullCommandStr[i].ToString())) 
                      //  { 
                        letterToRepeat = fullCommandStr[i].ToString();
                    //}
                        continue;
                    }

                    for (int j = i; j < fullCommandStr.Length; j++)
                    {
                        if (!(Int32.TryParse(fullCommandStr[j].ToString(), out number)) || (j == fullCommandStr.Length-1))
                        {
                            endOfNumberIndex = (j == fullCommandStr.Length - 1) ? j + 1 : j; 
                            int numberOfDigitsInNumber = endOfNumberIndex - startOfNumberIndex;

                            int numberOfLettersToAdd = (Int32.Parse(fullCommandStr.Substring(startOfNumberIndex, numberOfDigitsInNumber)))-1;//-1 so dont repeat existing character were copying
                            StringBuilder sb = new StringBuilder();
                            for (int k = 0; k < numberOfLettersToAdd; k++) 
                            {
                                sb.Append(letterToRepeat);
                            }
                            
                            

                            fullCommandStr = fullCommandStr.Insert(i, sb.ToString());
                            fullCommandStr = fullCommandStr.Remove(startOfNumberIndex+numberOfLettersToAdd, numberOfDigitsInNumber);
                            //Console.WriteLine(fullCommandStr);

                            i = i + numberOfLettersToAdd -1;
                            //Console.WriteLine("string is now : " + fullCommandStr +  "  i: " + i.ToString() + "    and break to i loop");


                            break;
                            

                        }
                    }

                }

                return fullCommandStr; 
            }

            private CommandKeyValidation ValidateCommandKeySeq(String fullCommandStr)
            {


            fullCommandStr = CommandStringAsLettersOnly(fullCommandStr);

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
            if (!resultOfCommandSequenceValidation.Valid) { resultOfCommandSequenceValidation.ErrorText = commandNotRecognisedString + resultOfCommandSequenceValidation.ErrorText +  " in command string : " +fullCommandStr + noLocationChange + validStringRequest + ReportLocationSingleRover(); }
            
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

