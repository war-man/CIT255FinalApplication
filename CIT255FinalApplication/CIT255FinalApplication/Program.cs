﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Application
{
    class Program
    {        
        static void Main(string[] args)
        {
            //pull data from the API
            InitializeDataFileXML.PullDataApi();

            //instantiate the controller
            Controller.Controller appController = new Controller.Controller();
        }
    }
}
