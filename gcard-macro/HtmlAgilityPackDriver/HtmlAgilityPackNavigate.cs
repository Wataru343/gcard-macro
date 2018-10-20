using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace gcard_macro.WebDriber
{
    public partial class HtmlAgilityPackDriver : IWebDriver
    {
        private class HtmlAgilityPackNavigate : INavigation
        {
            internal enum Move
            {
                Forward,
                Back,
                Refresh,
                New
            }

            private HtmlAgilityPackDriver Driver_ { get; set; }

            public HtmlAgilityPackNavigate(HtmlAgilityPackDriver driver) => Driver_ = driver;

            public void Back() => Driver_.MovePage(Move.Back);

            public void Forward() => Driver_.MovePage(Move.Forward);

            public void GoToUrl(string url) => Driver_.MovePage(Move.New, url);

            public void GoToUrl(Uri url) => Driver_.MovePage(Move.New, url.AbsolutePath);

            public void GoToUrlGet(string url) => Driver_.MovePage(Move.New, url, "GET");

            public void GoToUrlPost(string url, byte[] data = null) => Driver_.MovePage(Move.New, url, "POST", data);


            public void Refresh() => Driver_.MovePage(Move.Refresh);
        }
    }
}
