﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    public class StatisticsBindingModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int ElementId { get; set; }
    }
}
