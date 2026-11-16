using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class HistoryResponse
    {
        public List<ButtonDto> history { get; set; }
        public long? count { get; set; }
        public int? page { get; set; }
        public long? total_items { get; set; }
    }
}