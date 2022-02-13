using JsonUtil;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Bili
{
    public class AuthWindow : Window
    {
        public string Challenge { get; private set; }
        public string Validate { get; private set; }
        public CookieCollection Cookies { get; private set; }

        public ManualResetEvent CodeObtainedEvent { get; private set; }
        private WebView2 AuthView { get; set; }

        CookieCollection cookieCollection;

        public AuthWindow(Uri source, CookieCollection cookieCollection)
        {
            Width = 300;
            Height = 300;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CodeObtainedEvent = new ManualResetEvent(false);

            AuthView = new WebView2();
            this.Content = AuthView;

            AuthView.CoreWebView2InitializationCompleted += AuthView_CoreWebView2InitializationCompleted;
            AuthView.Source = source;

            Challenge = Regex.Match(source.AbsoluteUri, "&challenge=([0-9a-z]+)").Groups[1].Value;

            this.cookieCollection = cookieCollection;
        }

        private async void AuthView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            Json.Value.Array carray = new Json.Value.Array();
            foreach (Cookie cookie in this.cookieCollection)
            {
                Json.Value.Object cobj = new Json.Value.Object()
                {
                    { "name", cookie.Name },
                    { "value", cookie.Value },
                    { "domain", cookie.Domain },
                    { "path", cookie.Path },
                    { "secure", cookie.Secure },
                    { "httpOnly", cookie.HttpOnly },
                    { "expires", ToUnixTimeStamp(cookie.Expires) }
                };
                string str = cobj.ToString();
                carray.Add(cobj);
            }

            Json.Value.Object @object = new Json.Value.Object()
            {
                { "cookies", carray }
            };

            string cookieRes = await AuthView.CoreWebView2.CallDevToolsProtocolMethodAsync("Network.setCookies", @object.ToString());

            AuthView.CoreWebView2.WebResourceResponseReceived += CoreWebView2_WebResourceResponseReceived;
            AuthView.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;
            AuthView.CoreWebView2.AddWebResourceRequestedFilter(null, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceContext.All);
        }

        public static long ToUnixTimeStamp(DateTime date)
        {
            DateTime originDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - originDate;
            return (long)Math.Floor(diff.TotalSeconds);
        }

        private void CoreWebView2_WebResourceRequested(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequestedEventArgs e)
        {
            
        }

        private async void CoreWebView2_WebResourceResponseReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceResponseReceivedEventArgs e)
        {
            if(e.Request.Uri.Contains("/ajax.php"))
            {
                Stream stream = await e.Response.GetContentAsync();
                StreamReader streamReader = new StreamReader(stream);
                string content = await streamReader.ReadToEndAsync();
                Console.WriteLine(content);

                Match match = Regex.Match(content, "\"validate\": \"([0-9a-z]+)\"");
                if(match.Success)
                {
                    Validate = match.Groups[1].Value;

                    string cookieRes = await AuthView.CoreWebView2.CallDevToolsProtocolMethodAsync("Network.getCookies", "{}");
                    Json.Value json = Json.Parser.Parse(cookieRes);
                    CookieCollection cookieCollection = new CookieCollection();
                    foreach (Json.Value cookie in json["cookies"])
                    {
                        string name = cookie["name"];
                        string value = cookie["value"];
                        string path = cookie["path"];
                        string domain = cookie["domain"];
                        cookieCollection.Add(new Cookie(name, value, path, domain));
                    }
                    Cookies = cookieCollection;

                    CodeObtainedEvent.Set();
                }
            }
        }

    }
}
