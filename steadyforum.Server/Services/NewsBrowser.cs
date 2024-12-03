using System.Collections.ObjectModel;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

public class NewsBrowser : INewsBrowser
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Tag { get; set; }
    public string Mediapath { get; set; }
    public string Reference { get; set; }

    public int TakeNews()
    {
        /*pre set*/

        Collection<string> keywords = new Collection<string>() { "hack group" };
        Collection<string> recource = new Collection<string>() { "https://www.securitylab.ru/", "https://t.me/s/SecLabNews" };
        string telegramToken = "";
        int newsCounter = 0;

        foreach (var link in recource)
        {

            /*request*/

            /*sample*/

            /*generade media*/

            /*past db*/
        }

        return newsCounter;
    }
}