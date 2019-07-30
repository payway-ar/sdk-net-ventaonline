using System.Collections.Generic;

namespace Decidir.Model
{
    public class GetAllCardTokensResponse
    {
        public List<Token> tokens { get; set; }

        public GetAllCardTokensResponse()
        {
            this.tokens = new List<Token>();
        }
    }
}
