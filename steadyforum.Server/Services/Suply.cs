using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using steadyforum.Server.Data;

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

    public HttpClient checkMaterialCount(int orderId)
    {
        //get source map from database
        //get storage source count from database
        if (_context.Warehouse == null) return NotFound();                     

        var ware = await _context
                .Order
                .Where(s => s.id == orderId)
                .FirstOrDefaultAsync();

        var composition = await _context
            .Composition
            .Where(s => s.id == ware.id)
            .FirstOrDefaultAsync();
        // id:1 ware-id:1 composition-type:type vendor-mail:@ count:20
        // id:2 ware-id:1 composition-type:type vendor-mail:@ count:20
        // id:3 ware-id:1 composition-type:type vendor-mail:@ count:20

        //crete excel less source
        //call postal service once at friday
        foreach source in composition{
            if (await _context
            .Stock
            .Where(s => s.wareId == composition.compositionType)
            .FirstOrDefaultAsync() < composition.count) CreatePurchaseList(composition);                
        }        
    }
    public JsonContent CreatePurchaseList()
    {
        // id or qr-code
        // create excel 
        // from 
        // to 
        // stock-owner
        // composition-type:type vendor-mail:@ count:20
    }

    public void addSuplyDay()
    {
        // put suply paper to database

        // at friday broadcast suply paper 

        // at friday hang tracker for every suply paper 
        var isAppend = steadyforum.Server.Services.Stock.TrackingMaterial(orderId, purchaseListFull);
    }
    public void addIncident(int orderId)
    {
        // user can add incident then will check stock and day expired
    }
}