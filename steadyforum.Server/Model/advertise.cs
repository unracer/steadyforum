using System.Collections.Generic;
using System;

namespace steadyforum.Server.Model
{
    public class Advertise
    {
        public int id { get; set; }
        public DateOnly Date { get; set; }
        public string? Uname { get; set; }
        public string idcontent { get; set; } /*reference to content model*/

        public bool exist(int id) { return false; }
    }
}
