using System.Collections.Generic;
using System;

namespace steadyforum.Server.Model
{
    public class Chat
    {
        public int id { get; set; }
        public DateOnly Readed { get; set; }
        public DateOnly Date { get; set; }
        public string? Uname { get; set; }
        public string chatname { get; set; }
        public string passwordhash { get; set; }
        public string userlist { get; set; }
        public string idcontent { get; set; } /*reference to content model*/
        public string message { get; set; }


        public void getContent(string chatname, string sessionid)
        {
            // check session expired
            // message = get * where chattname ==
            // for message { get * where idcontent == ;  }
            // return new Model
        }
        public void pushMessage(Chat chat)
        {
            if (exist(chat.id)) { }

            // push
        }

        public bool exist(int id) { return false; }
    }
}
