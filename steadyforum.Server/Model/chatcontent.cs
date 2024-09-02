namespace steadyforum.Server.Model
{
    public class Chatcontent
    {
        public int? id { get; set; }
        public int? Idcontent { get; set; }
        public DateOnly? Readed { get; set; }
        public DateOnly? Date { get; set; }
        public string? Uname { get; set; }
        public string? Text { get; set; } /* reference text to another table */
        public string? Mediapath { get; set; }
        public string? Geo { get; set; }

        public void getChatList(string name, string sessionid)
        {
            // get uname by sessionid
            // from user ttable get chatlist
        }
        /*public void pushMessage(Chat chat)
        {
            if (exist(chat.id)) { }

            // push
        }

        public Boolean exist(int id) { return false; }*/
    }
}
