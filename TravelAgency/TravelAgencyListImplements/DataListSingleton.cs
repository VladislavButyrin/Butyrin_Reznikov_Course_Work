using System.Collections.Generic;
using TravelAgencyListImplement.Models;

namespace TravelAgencyListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Tour> Tours { get; set; }
        public List<Excursion> Excursions { get; set; }
        public List<Group> Groups { get; set; }
        public List<Trip> Trips { get; set; }
        public List<Guide> Guides { get; set; }
        public List<Place> Places { get; set; }
        private DataListSingleton()
        {
            Tours = new List<Tour>();
            Excursions = new List<Excursion>();
            Groups = new List<Group>();
            Trips = new List<Trip>();
            Guides = new List<Guide>();
            Places = new List<Place>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
