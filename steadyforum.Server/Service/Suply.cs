using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using steadyforum.Server.Data;
namespace steadyforum.Server.Service {
    public class Suply
    {
        private readonly steadyforumServerContext _context;

        public Suply(steadyforumServerContext context)
        {
            _context = context;
        }
        public string Title { get; set; }

        //database have some count
        //this count should to fill
        //also database count will change from analytic
        //this service will automatic create excel suply act and used postal service to send vendor

        // get order to production (crm) -> order material -> logistic to storage -> receiving material -> trasfer material to work position -> load plc program -> logistic complite goods to storage -> show on marketplace -> logistic to customer and used crm service

        async public HttpClient checkMaterialCount(Order order)
        {
            /*order
             * id
             * fname
             * lname
             * mname
             * number
             * customerAddress
             * productId
             * state
             * expired
             */

            /*product map
             * id
             * fields *
             */

            //get source map from database
            //get storage source count from database
            if (_context.Warehouse == null) return NotFound();

            var ware = await _context
                    .Order
                    .Where(s => s.id == order.id)
                    .FirstOrDefaultAsync();

            var compositionMap = await _context
                .Composition
                .Where(s => s.id == ware.productId)
                .FirstOrDefaultAsync();

            // id:1 ware-id:1 composition-type:type count:20 vendor-mail:@ 
            // id:2 ware-id:1 composition-type:type count:20 vendor-mail:@ 
            // id:3 ware-id:1 composition-type:type count:20 vendor-mail:@ 

            /* compositionMap
             * json {
                 *      "8r7ry73e43" : {
                 *          "compositionTarget":"pack",
                 *          "compositionType":"poliester",
                 *          "compositionCount":"0.24",
                 *          "compositionMetric":"killo",
                 *      },
                 *      "i49573845d" : {
                 *          "compositionTarget":"item",
                 *          "compositionType":"iron",
                 *          "compositionCount":"10",
                 *          "compositionMetric":"killo",
                 *      },
                 * }
             */
            //crete excel less source
            //call postal service once at friday
            foreach (string source in compositionMap) {
                if (await _context
                    .Stock
                    .Where(s => s.wareId == compositionMap.compositionType)
                    .FirstOrDefaultAsync() < compositionMap.compositionCount
                    ) CreatePurchaseList(order.id, compositionMap.compositionType, compositionMap.compositionCount, compositionMap.compositionMetric);
            }
        }
        async public void CreatePurchaseList(string orderid, string type, int count)
        {
            // create excel 
            
            // shipmentid 290834
            // from _context.erpinfo.stockId
            // to vendor-mail:@
            // composition-type:type
            // composition-count:20
            // composition-metric:killo

            File.Copy(@"C:\inetpub\wwwroot\project.Web\Clients\Handlers\oxml-tpl\livreurs.xlsx", @"C:\inetpub\wwwroot\project.Web\Clients\Handlers\oxml-tpl\generated.xlsx", true);
            SpreadsheetDocument purchaseList = SpreadsheetDocument.Open(@"C:\inetpub\wwwroot\project.Web\Clients\Handlers\oxml-tpl\generated.xlsx", true)
            var shipmentid = _context.CustomerOrder.LastId();
            purchaseList.SetValue(3, 4, shipmentid);
            purchaseList.SetValue(5, 4, type);
            purchaseList.SetValue(6, 4, count.ToString());

            var vendor = await _context
                .Vendor
                .Where(s => s.vendorType == type)
                .ListOrDefaultAsunc();

            var minload = 100;
            var vendorEmail = "";
            foreach (int load in vendor)
            {
                if (load.load < minload) { minload = load.load; vendorid = load.email}
            }

            Postal.sendEmail(vendorEmail, purchaseList);

            var order = await _context
                .CustomerOrder
                .Where(s => s.id == orderid)
                .ListOrDefaultAsunc();

            JsonContent shipment = new JsonContent(
                new List<string> request {"",""},
                new List<string> receive {"",""},
            );

            order.shipment = (shipment);
            order.state = "requstedSource";
            await _context.SaveChangesAsync();
        }

        public void addIncident(int orderId)
        {
            // user can add incident then will check stock and day expired
        }
    }
}