using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class Model3dsResponse
    {
        public long id { get; set; }
        public long timeout { get; set; }
        public TargetThreeds target { get; set; }
        public HttpThreeds http { get; set; }
        public string status { get; set; }

    }
}
