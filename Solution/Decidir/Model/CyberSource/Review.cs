using System.Collections.Generic;

namespace Decidir.Model.CyberSource
{
    public class Review
    {
        public string date {  get; set; }
        public string reviewer {  get; set; }
        public List<ReviewComments> comments { get; set; }
    }
}



