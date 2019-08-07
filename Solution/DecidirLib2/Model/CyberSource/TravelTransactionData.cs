using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class TravelTransactionData
    {
        public string reservation_code { get; set; }
        public bool third_party_booking { get; set; }
        public string departure_city { get; set; }
        public string final_destination_city { get; set; }
        public bool international_flight { get; set; }
        public string frequent_flier_number { get; set; }
        public string class_of_service { get; set; }
        public int day_of_week_of_flight { get; set; }
        public int week_of_year_of_flight { get; set; }
        public string airline_code { get; set; }
        public string code_share { get; set; }
        public int airline_number_of_passengers { get; set; }
        public List<Passengers> passengers { get; set; }
        public List<DecisionManagerTravel> decision_manager_travel { get; set; }
        
        public TravelTransactionData()
        {   
            this.decision_manager_travel = new List<DecisionManagerTravel>();
            this.passengers = new List<Passengers>();
        }
    }
}