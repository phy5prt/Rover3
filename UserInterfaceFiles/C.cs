using Rover3.MoveCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rover3.MoveCommands.DriveCommandsNS;
using Rover3.MoveCommands.FaceCommandsNS;
using Rover3.MoveCommands.TurnCommandsNS;
using Rover3;
using Rover3.UserInterfaceFiles;

namespace Rover3.UserInterfaceFiles
{
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
            K k = new K(); //need put dictionary in own class
            bool validInput, validXY, validXYRange;
            do
            {
                DisplayText("Enter a single unique key to select your rover with. It must be none numeric and not aleady assigned");
                newRoverKey = GetUserInput();
                validInput = !(MoveCommandDicManager.KeyExistsInAMoveCommandDictionary(newRoverKey) || UserInterfaceDic.interfaceDic.ContainsKey(newRoverKey) || newRoverKey.Length != 1 || Char.IsDigit(newRoverKey, 0));

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
}
