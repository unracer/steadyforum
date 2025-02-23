using System.Collections.ObjectModel;
using System.Net.Mail;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

public class ManufactureQueue
{
    public string Title { get; set; }

    public int grabOrderMaterial(string position)
    {
        // grab qrcode position 
        // load plc program for material loader and printer

        // call StockAuditor.grabFinishedProduction(qr-code addres)
    }
}