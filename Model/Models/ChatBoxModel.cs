using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ChatBoxModel
    {
        public UserChat ToUser { set; get; }
        public List<MessageChat> Messages { set; get; }
    }
}
