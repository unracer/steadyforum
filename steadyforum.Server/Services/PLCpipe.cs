using System.Buffers;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Net;
using System.Security.Policy;
using System.Text;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class PLCpipe 
{
    public string Title { get; set; }
    //Siemens Simatic S7 Web Apps and API
    public int S7loader()
    {
        var url_api = "https://192.168.178.50/awp/AnalogInputs/api.io";
        var payload = "{ '\"webHMIData\".webHMI_DO0_User': str(do0).lower(), '\"webHMIData\".webHMI_DO1_User': str(do1).lower()}";
        var S7cert = "";

        HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url_api);
        httpRequest.Method = "POST";
        httpRequest.ContentType = "application/x-www-form-urlencoded";
        httpRequest.ContentLength = (payload+S7cert).Length;

        var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
        streamWriter.Write(payload, S7cert);
        streamWriter.Close();

        return (int)((HttpWebResponse)httpRequest.GetResponse()).StatusCode;
    }
}