using System.Collections.Generic;
using System;

namespace steadyforum.Server.Model
{
    public class Chat
    {
        public int Id { get; set; }
        public int? Idcontent { get; set; } /*reference to content model*/
        /*public int? IrcMode { get; set; } */ // websocket dont sent data to database like internet realy chat
        public string? Chatname { get; set; }
        public string? Passwordhash { get; set; }
        public string? Userlist { get; set; } /*MAKE ARRAY*/

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
            if (exist(chat.Id)) { }

            // push
        }

        public bool exist(int id) { return false; }
    }
}
