namespace steadyforum.Server.Model
{
    public class Chatcontent
    {
        public int id { get; set; }
        public int idcontent { get; set; }
        public DateOnly Readed { get; set; }
        public DateOnly Date { get; set; }
        public string? Uname { get; set; }
        public string text { get; set; }
        public string mediapath { get; set; }
        public string geo { get; set; }

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
