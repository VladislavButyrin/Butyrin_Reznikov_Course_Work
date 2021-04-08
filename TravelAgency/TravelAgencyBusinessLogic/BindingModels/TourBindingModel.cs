﻿using System;
using System.Collections.Generic;



namespace TravelAgencyBusinessLogic.BindingModels
{
    public class TourBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string PlaceOfResidence { get; set; }

        public string PlaceOfDeparture { get; set; }

        public DateTime DateOfDeparture { get; set; }

    }
}
