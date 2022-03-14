using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInBackend.Model.Utility
{
    public class NotificationProperty
    {
        public NotificationProperty()
        {
            Icon = "https://cdn2.iconfinder.com/data/icons/world-flag-icons/256/Flag_of_Indonesia.png";
        }

        public string Title { get; set; }
        public string Body { get; set; }
        public string Icon { get; set; }
        public string ChannelId { get; set; }
        public string ClickAction { get; set; }
    }
}
