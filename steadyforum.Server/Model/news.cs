using System.Collections.Generic;
using System;

namespace steadyforum.Server.Model
{
    public class News 
    {
        public int id { get; set; }
        public DateOnly Date { get; set; }
        public string? Title { get; set; }
        public string? Tag { get; set; }
        public string? Content { get; set; }
        public string? Mediapath { get; set; }
        public string? Reference { get; set; }
        public int? Like { get; set; }
        public int? Dislike { get; set; }
        public int? Repost { get; set; }
        public bool exist(int id) { return false; }
    }
}
