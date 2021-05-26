using System.Collections.Generic;

namespace TravelAgencyBusinessLogic.HelperModels.Guarantor
{
    class WordParagraph
    {
        public List<(string, WordTextProperties)> Texts { get; set; }
        public WordTextProperties TextProperties { get; set; }
    }
}
