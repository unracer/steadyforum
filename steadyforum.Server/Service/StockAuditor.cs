using System;
using System.Collections.ObjectModel;
using System.Net.Mail;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace steadyforum.Server.Service
{
    public class StockAuditor
    {
        public string Title { get; set; }
        private Random random = new Random();

        public string receivedMaterial(ShipmentOrder shipmentOrder)
        {
            // transfer material

            // if date order expired then call suply.incident
            // if not all material in order accepted then exit 

            // find order and mark as accepted
            // call manufacture service with give him qr-code stock position

            // call staff and show him position

            string position = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 20).Select(s => s[random.Next(s.Length)]).ToArray());
                        
            var order = await _context
                .CustomerOrder
                .Where(s => s.id == shipmentOrder.orderid)
                .ListOrDefaultAsunc();

            if (order.expired >= Date.Today()) Suply.Incedent(order);

            foreach (var shipment in order.shipment) {
                foreach (var request in shipment.request) { 
                    if (request !in shipment.receive) return "not all material accepted";
                }
            }

            order.state = "accepted all material";

            ManufactureQueue.grabOrderMaterial(position);

            return position;
        }
        public int grabFinishedProduction(string position)
        {
            // check if production for marketplace then call suplay.readyMarketplaceProduction(qr-code-position)
            // call erp-manager and give him qr-code stock position
        }
    }
}