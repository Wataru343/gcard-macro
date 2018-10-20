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
        private class HtmlAgilityPackOptions : IOptions
        {
            private HtmlAgilityPackDriver Driver_ { get; set; }
            public HtmlAgilityPackOptions(HtmlAgilityPackDriver driver) => Driver_ = driver;

            public ICookieJar Cookies => Driver_.Cookies_;

            public IWindow Window => new HtmlAgilityPackWindow();

            public ILogs Logs => throw new NotImplementedException();

            public ITimeouts Timeouts()
            {
                throw new NotImplementedException();
            }
        }
    }
}
