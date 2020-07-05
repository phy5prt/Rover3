﻿using Rover3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover3
{
    //this class shoul just be called report

//A report should be:
        //Rover A (ABC)
        //Route check was successful (All rovers)
                        
        
        // per rover 
        //rover move possible
       //check unsuccessful at 
                            

        //If successful - 
            //rover exectuted string
            //rover position
            //rover facing

//A group rover report should be   //no group rover 
        //The rovers   -   could not / were moved
        // rover reports


        
   class RoverTasksValidation //should this be an interface
    {
        public RoverTasksValidation(string roverValidatingName) 
        {
            NameOfRover = roverValidatingName;
        }


        private LocationInfo _whereLocationBecomesInvalid;

        private bool _commandExecutionSuccess;
        public bool CommandsExecutionSuccess
        {
            get { return this._commandExecutionSuccess; }
            set {
                //should this just be dont manually so dont confuse things
                this._commandExecutionSuccess = value;
                if (value == false) { RoverMoved = false; }
                //not the otherway around though because may not have moved if just given an empty  command
            } 
        }
        bool _roverMoved = false;
        public bool RoverMoved { get { return this._roverMoved; } set { this._roverMoved = value; } } 
        public int InvalidCommandIndex { get; set; }
        public string NameOfRover { get; set; } 
        public string NameOfRoverCollidedWith { get; set; }
        public LocationInfo TaskEndLocation { get; set; }
        public LocationInfo WhereCommandBecomesInvalid { get => _whereLocationBecomesInvalid; set => _whereLocationBecomesInvalid = value; }

        //private string _validationReport;
        //public string ValidationReport 
        //{
        //    get
        //    {
               
        //            return _validationReport;
        //     }

        //}
        
        ///Add operator overload
      
    }

}
