﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailorApp.Web.ViewModels
{
    public class AssignedMeasurements
    {
        public int MeasurementID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}