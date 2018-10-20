using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace gcard_macro.WebDriber
{
    public partial class HtmlAgilityPackDriver : IWebDriver
    {
        private class HtmlAgilityPackCookieJar : ICookieJar
        {
            private HtmlAgilityPackDriver Driver_ { get; set; }
            public ReadOnlyCollection<Cookie> AllCookies { get; set; }

            public HtmlAgilityPackCookieJar(HtmlAgilityPackDriver driver)
            {
                Driver_ = driver;
                AllCookies = new ReadOnlyCollection<Cookie>(new List<Cookie>());
            }

            public void AddCookie(Cookie cookie)
            {
                AllCookies = new ReadOnlyCollection<Cookie>(new List<Cookie>(AllCookies) { cookie });
                Driver_.ReloadCookie();
            }

            public void DeleteAllCookies()
            {
                AllCookies = new ReadOnlyCollection<Cookie>(new List<Cookie>());
                Driver_.ReloadCookie();
            }

            public void DeleteCookie(Cookie cookie)
            {
                List<Cookie> newCookie = new List<Cookie>(AllCookies);
                newCookie.Remove(cookie);
                AllCookies = new ReadOnlyCollection<Cookie>(newCookie);
            }

            public void DeleteCookieNamed(string name)
            {
                List<Cookie> newCookie = new List<Cookie>(AllCookies);
                newCookie.RemoveAll(e => e.Name == name);
                AllCookies = new ReadOnlyCollection<Cookie>(newCookie);
            }

            public Cookie GetCookieNamed(string name) => AllCookies.ToList().Find(e => e.Name == name);
        }
    }
}
