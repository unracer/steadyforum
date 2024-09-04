namespace steadyforum.Server.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Uname { get; set; }
        public string? Passwordhash { get; set; }
        public string? Chatlist { get; set; }
        public string? Sessionid { get; set; }
        public DateTime SessionCreate { get; set; }

        // dont know how to request data from db from model (here) but easy bussines logic code can transfer
        public void getChatList(string sessionid)
        {
            // check session expired
            // return chatlist by sessionid
        }

    }
}
