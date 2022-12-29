using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class Auth3dsResponse
    {
		public String id { get; set; }
		public String status { get; set; }
		public String authentication_value { get; set; }
		public String commerce_indicator { get; set; }
		public String protocol_version { get; set; }
		public String directory_server_transaction_id { get; set; }
	}
}
