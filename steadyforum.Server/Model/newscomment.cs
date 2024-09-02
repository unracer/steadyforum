using System.Collections.Generic;
using System;

namespace steadyforum.Server.Model
{
    public class Newscomment
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Uname { get; set; }
        public string? Message { get; set; }
        public int? Subcommentid { get; set; }
        public int? Newsid { get; set; }
        public string? Mediapath { get; set; }
    }
}
