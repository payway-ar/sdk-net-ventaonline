using System.Collections.Generic;
using Decidir.Model;

namespace DecidirExample.Models
{
    public class CardTokenBsaDTO
    {
        public int AmbienteId { get; set; }
        public string privateApiKey { get; set; }
        public string publicApiKey { get; set; }
        public CardTokenBsa cardTokenBsa { get; set; }
    }
}