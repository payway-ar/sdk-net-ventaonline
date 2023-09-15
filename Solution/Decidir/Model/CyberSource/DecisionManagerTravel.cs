using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class DecisionManagerTravel
    {
        public string complete_route { get; set; }
        public string journey_type { get; set; }
        public DepartureDate departure_date { get; set; }

        public DecisionManagerTravel()
        {
        }
    }
}
