using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using OpenQA.Selenium;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

namespace gcard_macro.WebDriber
{
    public partial class HtmlAgilityPackDriver : IWebDriver
    {
        private LinkedList<string> Urls_ { get; set; }
        private LinkedList<string> ForwardUrls_ { get; set; }
        private WebClient Client_ { get; set; }
        private HtmlDocument HtmlDoc_ { get; set; }
        private HtmlAgilityPackCookieJar Cookies_ { get; set; }
        private string UserAgent_ { get; set; }

        public string Url { get => Urls_.Last(); set => throw new NotImplementedException(); }

        public string Title => HtmlDoc_.DocumentNode.SelectSingleNode("//title").InnerText;

        public string PageSource { get; private set; }

        public string CurrentWindowHandle => "";

        public ReadOnlyCollection<string> WindowHandles => new ReadOnlyCollection<string>(new List<string>());

        public HtmlAgilityPackDriver(string userAgent = "")
        {
            HtmlDoc_ = new HtmlDocument();
            Urls_ = new LinkedList<string>();
            ForwardUrls_ = new LinkedList<string>();
            UserAgent_ = userAgent;
            Cookies_ = new HtmlAgilityPackCookieJar(this);

            ReloadWebClient();
        }

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public IWebElement FindElement(By by)
        {
            (HtmlAgilityPackElement.Command command, string path) = ParseCommand(by);
            HtmlAgilityPackElement element = HtmlAgilityPackElement.CreateElement(command, path, HtmlDoc_.DocumentNode, this);
            if (element.HtmlNode == null) throw new Exception();
            return element;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            (HtmlAgilityPackElement.Command command, string path) = ParseCommand(by);
            return new ReadOnlyCollection<IWebElement>(HtmlAgilityPackElement.CreateElements(command, path, HtmlDoc_.DocumentNode, this).Select(e => e as IWebElement).ToList());

            throw new NotImplementedException();
        }

        public IOptions Manage() => new HtmlAgilityPackOptions(this);

        public INavigation Navigate() => new HtmlAgilityPackNavigate(this);

        public void Quit()
        {
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotImplementedException();
        }

        private void Get(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers[HttpRequestHeader.Cookie] = string.Join("; ", Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
            request.UserAgent = UserAgent_;
            request.AllowAutoRedirect = true;
  
            Download(request);
        }

        private void Post(string url, byte[] data)
        {
            if (data == null) data = new byte[0];

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers[HttpRequestHeader.Cookie] = string.Join("; ", Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
            request.UserAgent = UserAgent_;
            request.Method = WebRequestMethods.Http.Post;
            request.AllowAutoRedirect = true;
            request.ContentLength = data.Length;


            using (Stream stream = request.GetRequestStream())
                stream.Write(data, 0, data.Length);


            Download(request);
        }

        private bool Download(HttpWebRequest request)
        {
            try
            {
                using (WebResponse response = request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    if (response.ResponseUri.ToString() != request.RequestUri.ToString())
                        Urls_.AddLast(response.ResponseUri.ToString());

                    StringBuilder sb = new StringBuilder(reader.ReadToEnd());
                    sb.Replace("\t", "");
                    sb.Replace("\r", "");
                    sb.Replace("\n", "");

                    PageSource = sb.ToString();

                    HtmlDoc_.LoadHtml(PageSource);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void MovePage(HtmlAgilityPackNavigate.Move move, string url = null, string method = "", byte[] data = null)
        {
            string last = "";
            switch (move)
            {
                case HtmlAgilityPackNavigate.Move.Forward:
                    if (ForwardUrls_.Count() > 0)
                    {
                        last = ForwardUrls_.Last();
                        Urls_.AddLast(last);
                        ForwardUrls_.RemoveLast();
                        Get(Urls_.Last());
                    }
                    break;
                case HtmlAgilityPackNavigate.Move.Back:
                    last = Urls_.Last();
                    ForwardUrls_.AddLast(last);
                    Urls_.RemoveLast();
                    Get(Urls_.Last());
                    break;
                case HtmlAgilityPackNavigate.Move.Refresh:
                    Get(Url);
                    break;
                case HtmlAgilityPackNavigate.Move.New:
                    Urls_.AddLast(url);

                    while (Urls_.Count > 100)
                        Urls_.RemoveFirst();

                    ForwardUrls_.Clear();

                    if (method == "" || method == WebRequestMethods.Http.Get) Get(Url);
                    else if (method == WebRequestMethods.Http.Post) Post(Url, data);
                    break;
                default:
                    break;
            }
        }

        private void ReloadWebClient()
        {
            Client_ = new WebClient();
            Client_.Encoding = Encoding.UTF8;
            Client_.Headers[HttpRequestHeader.Cookie] = string.Join("; ", Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
        }

        private void ReloadCookie()
        {
            ReloadWebClient();
        }

        private static (HtmlAgilityPackElement.Command command, string path) ParseCommand(By by)
        {
            Regex r = new Regex(@"^By.(?<command>.+): (?<path>.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = r.Matches(by.ToString());

            if (mc.Count > 0)
            {
                string commandStr = mc[0].Groups["command"].Value;
                string path = mc[0].Groups["path"].Value;
                HtmlAgilityPackElement.Command command = HtmlAgilityPackElement.Command.XPath;
                switch (commandStr)
                {
                    case "ClassName[Contains]": command = HtmlAgilityPackElement.Command.ClassName; break;
                    case "XPath": command = HtmlAgilityPackElement.Command.XPath; break;
                    default: break;
                }

                return (command, path);
            }
            else
            {
                throw new NoSuchElementException();
            }
        }

        private static string RemoveTag(string hrml)
        {
            Regex r = new Regex(@"(<.*?>)+");
            MatchCollection mc = r.Matches(hrml);
            StringBuilder sb = new StringBuilder(hrml);

            foreach (Match m in mc)
                for (int i = 0; i < m.Groups.Count; i++)
                    for (int j = 0; j < m.Groups[i].Captures.Count; j++)
                        sb.Replace(m.Groups[i].Captures[j].Value, "");

            return sb.ToString().Trim(new char[] { ' ', '\n', '\r', '\t' });
        }
    }
}
