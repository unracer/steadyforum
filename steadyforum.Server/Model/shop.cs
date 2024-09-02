using System.Collections.Generic;
using System;
using System.Security.Policy;

namespace steadyforum.Server.Model
{
    public class Shop
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Unitid { get; set; }
        public DateTime? Datecreate { get; set; }
        public string? Mediapath { get; set; }
        public string? Paymentsystem { get; set; }
        public Url? Externallink { get; set; }
        public string? Description { get; set; }
        public bool? IsSale { get; set; }
    }
}
