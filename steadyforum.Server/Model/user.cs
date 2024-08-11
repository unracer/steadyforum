namespace steadyforum.Server.Model
{
    public class User
    {
        public int id { get; set; }
        public string uname { get; set; }
        public string passwordhash { get; set; }
        public string chatlist { get; set; }
        public string sessionid { get; set; }
        public DateTime sessionCreate { get; set; }

        // dont know how to request data from db from model (here) but easy bussines logic code can transfer
        public void getChatList(string sessionid)
        {
            // check session expired
            // return chatlist by sessionid
        }

    }
}
