using System.Collections.ObjectModel;
using System.Net.Mail;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace steadyforum.Server.Service
{
    public class ManufactureQueue
    {
        public string Title { get; set; }

        public void grabOrderMaterial(string position)
        {
            // grab qrcode position using staff
            // load plc program for material loader and printer

            // call StockAuditor.grabFinishedProduction(qr-code addres)
        }
    }
}