using System.Collections.Generic;
using System;

namespace steadyforum.Server.Model
{
    public class Chat
    {
        public int id { get; set; }
        public int? Idcontent { get; set; } /*reference to content model*/
        public string? Chatname { get; set; }
        public string? Passwordhash { get; set; }
        public string? Userlist { get; set; }

        /*public string[] userlist { get; set; }*/ // feuture

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
