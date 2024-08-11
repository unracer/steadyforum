using System.Collections.Generic;
using System;

namespace steadyforum.Server.Model
{
    public class News
    {
        public int id { get; set; }
        public DateOnly Date { get; set; }
        public string? Uname { get; set; }


        public bool exist(int id) { return false; }
    }
}
