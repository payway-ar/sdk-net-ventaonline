﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class BatchClosureResponse
    {

        public string batch_id { get; set; }
        public string file_name { get; set; }

        public List<BatchClosureError> errors { get; set; }

    }
}