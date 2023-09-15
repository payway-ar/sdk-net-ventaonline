using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class Auth3dsData
    {
		public String device_type { get; set; }
		public String accept_header { get; set; }
		public String user_agent { get; set; }
		public String ip { get; set; }
		public Boolean java_enabled { get; set; }
		public String language { get; set; }
		public String color_depth { get; set; }
		public long screen_height { get; set; }
		public long screen_width { get; set; }
		public long time_zone_offset { get; set; }
	}
}
