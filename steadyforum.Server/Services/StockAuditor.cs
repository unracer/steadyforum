using System.Collections.ObjectModel;
using System.Net.Mail;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

public class StockAuditor
{
    public string Title { get; set; }

    public int receivedMaterial()
    {
        // find order and mark as accepted
        // send to stock with qr-code position

        // then if date order expired then call suply.incident
        // then if not all material in order accepted then exit 

        // call manufacture service with give him qr-code stock position
    }
    public int grabFinishedProduction(string position)
    {
        // check if production for marketplace then call suplay.readyMarketplaceProduction(qr-code-position)
        // call logistic partners and give him qr-code stock position, customer adress post index
    }
}