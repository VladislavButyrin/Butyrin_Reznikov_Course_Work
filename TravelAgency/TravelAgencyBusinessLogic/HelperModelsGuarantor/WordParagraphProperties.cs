﻿using DocumentFormat.OpenXml.Wordprocessing;

namespace TravelAgencyBusinessLogic.HelperModels.Guarantor
{
    class WordParagraphProperties
    {
        public string Size { get; set; }
        public bool Bold { get; set; }
        public JustificationValues JustificationValues { get; set; }
    }
}
