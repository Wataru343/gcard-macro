using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HtmlAgilityPack;
using System.Net;

namespace gcard_macro.WebDriber
{
    public partial class HtmlAgilityPackDriver : IWebDriver
    {
        private class HtmlAgilityPackElement : IWebElement
        {
            internal enum Command
            {
                ClassName,
                CssSelector,
                Id,
                LinkText,
                Name,
                PartialLink,
                TagName,
                XPath
            }

            private HtmlNode HtmlNode_ { get; set; }
            private HtmlAgilityPackDriver Driver_ { get; set; }

            public HtmlAgilityPackElement(HtmlNode node, HtmlAgilityPackDriver driver)
            {
                HtmlNode_ = node;
                Driver_ = driver;
            }

            public HtmlAgilityPackElement(Command command, string path, HtmlNode node, HtmlAgilityPackDriver driver)
            {
                HtmlNode_ = GetNode(command, path, node);
                Driver_ = driver;
            }

            public static HtmlAgilityPackElement CreateElement(Command command, string path, HtmlNode node, HtmlAgilityPackDriver driver) => new HtmlAgilityPackElement(command, path, node, driver);

            public static IEnumerable<HtmlAgilityPackElement> CreateElements(Command command, string path, HtmlNode node, HtmlAgilityPackDriver driver) => GetNodes(command, path, node).Select(e => new HtmlAgilityPackElement(e, driver));

            public string TagName => HtmlNode_.Name;

            public string Text => HtmlNode_.InnerHtml.Trim(new char[] { ' ' });

            public bool Enabled => HtmlNode_ != null;

            public bool Selected => throw new NotImplementedException();

            public Point Location => new Point();

            public Size Size => new Size();

            public bool Displayed => false;

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public void Click()
            {
                throw new NotImplementedException();
            }

            public IWebElement FindElement(By by)
            {
                (Command command, string path) = ParseCommand(by);
                IWebElement element = CreateElement(command, path, HtmlNode_, Driver_);
                return element.Enabled ? element : throw new NoSuchElementException();
            }

            public ReadOnlyCollection<IWebElement> FindElements(By by)
            {
                (Command command, string path) = ParseCommand(by);
                return new ReadOnlyCollection<IWebElement>(CreateElements(command, path, HtmlNode_, Driver_).Select(e => e as IWebElement).ToList());
            }

            public string GetAttribute(string attributeName)
            {
                string ret = HtmlNode_.GetAttributeValue(attributeName, "");

                if (attributeName == "href" || attributeName == "action") return string.Format("{0}/{1}", new Uri(Driver_.Url).GetLeftPart(UriPartial.Authority), ret.Replace("amp;", ""));
                else return ret;
            }

            public string GetCssValue(string propertyName)
            {

                throw new NotImplementedException();
            }

            public string GetProperty(string propertyName)
            {
                throw new NotImplementedException();
            }

            public void SendKeys(string text)
            {
                throw new NotImplementedException();
            }

            public void Submit()
            {
                string url = GetAttribute("action");
                string method = GetAttribute("method");

                if (url != "")
                {
                    var nodes = HtmlNode_.Descendants("input").ToArray();

                    if (method == "" || method == WebRequestMethods.Http.Get)
                    {
                        url = nodes.Where(e => e.GetAttributeValue("type", "") == "hidden").Select(e => new { id = e.GetAttributeValue("name", ""), value = e.GetAttributeValue("value", "") }).Aggregate(url + "?", (n, e) => n + string.Format("{0}={1}&", e.id, e.value)).TrimEnd(new char[] { '&' });
                        (Driver_.Navigate() as HtmlAgilityPackNavigate).GoToUrlGet(url);
                    }
                    else if(method == WebRequestMethods.Http.Post)
                    {
                        string param = nodes.Where(e => e.GetAttributeValue("type", "") == "hidden").Select(e => new { id = e.GetAttributeValue("name", ""), value = e.GetAttributeValue("value", "") }).Aggregate("", (n, e) => n + string.Format("{0}={1}&", e.id, e.value)).TrimEnd(new char[] { '&' });
                        byte[] data = Encoding.ASCII.GetBytes(param);
                        (Driver_.Navigate() as HtmlAgilityPackNavigate).GoToUrlPost(url, data);
                    }
                }
            }

            private static HtmlNode GetNode(Command command, string path, HtmlNode node)
            {
                switch (command)
                {
                    case Command.ClassName:
                    case Command.CssSelector:
                    case Command.Id:
                    case Command.LinkText:
                    case Command.Name:
                    case Command.PartialLink:
                    case Command.TagName:
                    case Command.XPath: return node.SelectSingleNode(path);
                    default: return node.SelectSingleNode(path);
                }
            }

            private static IEnumerable<HtmlNode> GetNodes(Command command, string path, HtmlNode node)
            {
                switch (command)
                {
                    case Command.ClassName:
                    case Command.CssSelector:
                    case Command.Id:
                    case Command.LinkText:
                    case Command.Name:
                    case Command.PartialLink:
                    case Command.TagName:
                    case Command.XPath: return node.SelectNodes(path) ?? new List<HtmlNode>() as IEnumerable<HtmlNode>;
                    default: return node.SelectNodes(path);
                }
            }


        }
    }
}
