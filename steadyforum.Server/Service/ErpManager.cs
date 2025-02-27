using System.Buffers;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Net;
using System.Security.Policy;
using System.Text;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Hosting;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace steadyforum.Server.Service
{
    public class ErpManager
    {
        public string Title { get; set; }
        //Siemens Simatic S7 Web Apps and API
        public void CreateCrmOrder(Order order)
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
             * shipmentList
             * expired
             */

            if (StockAuditor.checkAbortedOrder(order.id))
            {
                order.state = "haveOnStock";

                _context.CustomerOrder.Add(order);
                await _context.SaveChangesAsync();

                Postal.TransferCustomerOrder(createExcel(order.PersonalData, order.targetAddress, qrcodeStockPosition));                
            }

            order.state = "suplyMaterial";

            _context.CustomerOrder.Add(order);
            await _context.SaveChangesAsync();

            Suply.checkMaterialCount(order);            
        }

        public int publishCrmOrder(int stockQrCodePosition)
        {
            if (abortOrder)
            {

            }

            postal.TransferCustomerOrder(createExcel(order.PersonalData, order.targetAddress, qrcodeStockPosition));

            // call crm.addAction(order, type=deliverOder)
        }

        public int handleCrmTransaction(order, snpkType)
        {

        }
    }
}